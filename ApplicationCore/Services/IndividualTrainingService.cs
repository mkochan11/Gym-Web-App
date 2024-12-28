using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Training;
using ApplicationCore.Specifications;
using Ardalis.Result;

namespace ApplicationCore.Services
{
    public class IndividualTrainingService : IIndividualTrainingService
    {
        IRepository<Client> _clientRepository;
        IRepository<GymMembership> _gymMembershipRepository;
        IRepository<MembershipPlan> _membershipPlanRepository;
        IRepository<IndividualTraining> _individualTrainingRepository;
        IRepository<PersonalTrainer> _personalTrainerRepository;
        IMembershipService _membershipService;

        public IndividualTrainingService(
            IRepository<Client> clientRepository,
            IRepository<GymMembership> gymMemberhsipRepository,
            IRepository<MembershipPlan> membershipPlanRepository,
            IRepository<IndividualTraining> individualTrainingRepository,
            IRepository<PersonalTrainer> personalTrainerRepository,
            IMembershipService membershipService
            )
        {
            _clientRepository = clientRepository;
            _gymMembershipRepository = gymMemberhsipRepository;
            _membershipPlanRepository = membershipPlanRepository;
            _individualTrainingRepository = individualTrainingRepository;
            _membershipService = membershipService;
            _personalTrainerRepository = personalTrainerRepository;
        }

        public async Task<Result> CancelReservation(int trainingId, string userId)
        {
            var _clientSpec = new FindClientByUserId(userId);
            var client = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            var training = await _individualTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            if (training.ClientId != null && training.ClientId != client.Id)
            {
                return Result.Error("Trening został zarezerwowany przez innego klienta");
            }
            else if (training.ClientId != null && training.ClientId == client.Id)
            {
                training.ClientId = null;
                await _individualTrainingRepository.UpdateAsync(training);
                return Result.Success();
            }
            else
            {
                return Result.Error("Trening nie jest zarezerwowany");
            }
        }

        public async Task<Result> CreateTraining(NewIndividualTrainingModel model, string userId)
        {
            var _trainerSpec = new FindPersonalTrainerByUserId(userId);
            var trainer = await _personalTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            if (model.IsCyclic)
            {
                var trainings = new List<IndividualTraining>();
                DateTime startDate = model.Date;
                DateTime repeatUntilDate = new DateTime(year: 2025, month: 3, day: 31);

                if (model.Repeatability == "everyday")
                {
                    int totalDays = (repeatUntilDate - startDate).Days;

                    for (int i = 0; i <= totalDays; i++)
                    {
                        var training = new IndividualTraining
                        {
                            PersonalTrainerId = trainer.Id,
                            Date = startDate.AddDays(i),
                            Description = model.Description is null ? "" : model.Description,
                            Duration = model.Duration,
                        };

                        training.Date = training.Date.AddHours(model.Hour.Hour);
                        trainings.Add(training);
                    }
                }
                else if (model.Repeatability == "everyWeek")
                {
                    int totalWeeks = (int)((repeatUntilDate - startDate).Days / 7);

                    for (int i = 0; i <= totalWeeks; i++)
                    {
                        var training = new IndividualTraining
                        {
                            PersonalTrainerId = trainer.Id,
                            Date = startDate.AddDays(i * 7),
                            Description = model.Description is null ? "" : model.Description,
                            Duration = model.Duration,
                        };

                        training.Date = training.Date.AddHours(model.Hour.Hour);
                        trainings.Add(training);
                    }
                }
                else if (model.Repeatability == "everyMonth")
                {
                    int totalMonths = (repeatUntilDate.Year - startDate.Year) * 12 + repeatUntilDate.Month - startDate.Month;

                    for (int i = 0; i <= totalMonths; i++)
                    {
                        var training = new IndividualTraining
                        {
                            PersonalTrainerId = trainer.Id,
                            Date = startDate.AddMonths(i),
                            Description = model.Description is null ? "" : model.Description,
                            Duration = model.Duration,
                        };

                        training.Date = training.Date.AddHours(model.Hour.Hour);
                        trainings.Add(training);
                    }
                }
                else
                {
                    return Result.Error("Podano niepoprawną cykliczność");
                }

                await _individualTrainingRepository.AddRangeAsync(trainings);
                return Result.Success();

            }
            else
            {
                var training = new IndividualTraining
                {
                    PersonalTrainerId = trainer.Id,
                    Date = model.Date,
                    Description = model.Description is null ? "" : model.Description,
                    Duration = model.Duration,
                };

                training.Date = training.Date.AddHours(model.Hour.Hour);

                await _individualTrainingRepository.AddAsync(training);
                return Result.Success();
            }
        }

        public async Task<Result> DeleteTraining(int trainingId, string userId)
        {
            var _trainerSpec = new FindPersonalTrainerByUserId(userId);
            var trainer = await _personalTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var training = await _individualTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            await _individualTrainingRepository.DeleteAsync(training);

            return Result.Success();
        }

        public async Task<Result> Reserve(int trainingId, string userId)
        {
            var _clientSpec = new FindClientByUserId(userId);
            var client = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            var membership = await _membershipService.GetActiveMembership(client.Id);

            if (membership == null)
            {
                return Result.Error("Nie znaleziono aktywnego karnetu dla danego klienta");
            }

            var membershipPlan = await _membershipPlanRepository.GetByIdAsync(membership.MembershipPlanId);

            if (membershipPlan == null || !membershipPlan.CanReserveTrainings)
            {
                return Result.Error("Nie znaleziono planu lub plan nie pozwala na rezerwację zajęć");
            }

            var training = await _individualTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }
            
            if(training.ClientId != null)
            {
                var trainingClient = await _clientRepository.GetByIdAsync(training.ClientId);
                if (trainingClient != null)
                {
                    return Result.Error("Trening jest już zarezerwowany");
                }
                else
                {
                    training.ClientId = client.Id;
                    await _individualTrainingRepository.UpdateAsync(training);
                    return Result.Success();
                }
            }

            training.ClientId = client.Id;
            await _individualTrainingRepository.UpdateAsync(training);
            return Result.Success();
        }

        public async Task<Result> UpdateTraining(EditIndividualTrainingModel model, string userId)
        {
            var _trainerSpec = new FindPersonalTrainerByUserId(userId);
            var trainer = await _personalTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var training = await _individualTrainingRepository.GetByIdAsync(model.Id);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            training.Date = model.Date;
            training.Duration = model.Duration;
            training.Description = model.Description is null ? "" : model.Description;
            training.Date = training.Date.AddHours(model.Hour.Hour);

            await _individualTrainingRepository.UpdateAsync(training);
            return Result.Success();
        }
    }
}
