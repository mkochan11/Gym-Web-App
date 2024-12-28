using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Training;
using ApplicationCore.Specifications;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class GroupTrainingService : IGroupTrainingService
    {
        IRepository<Client> _clientRepository;
        IRepository<GymMembership> _gymMembershipRepository;
        IRepository<MembershipPlan> _membershipPlanRepository;
        IRepository<GroupTraining> _groupTrainingRepository;
        IRepository<GroupTrainingParticipation> _groupTrainingParticipationRepository;
        IRepository<GroupTrainer> _groupTrainerRepository;
        IMembershipService _membershipService;

        public GroupTrainingService(
            IRepository<Client> clientRepository,
            IRepository<GymMembership> gymMemberhsipRepository,
            IRepository<MembershipPlan> membershipPlanRepository,
            IRepository<GroupTraining> groupTrainingRepository,
            IRepository<GroupTrainingParticipation> groupTrainingParticipationRepository,
            IRepository<GroupTrainer> groupTrainerRepository,
            IMembershipService membershipService
            )
        {
            _clientRepository = clientRepository;
            _gymMembershipRepository = gymMemberhsipRepository;
            _membershipPlanRepository = membershipPlanRepository;
            _groupTrainingRepository = groupTrainingRepository;
            _groupTrainingParticipationRepository = groupTrainingParticipationRepository;
            _membershipService = membershipService;
            _groupTrainerRepository = groupTrainerRepository;
        }

        public async Task<Result> CancelPlace(int trainingId, string userId)
        {
            var _clientSpec = new FindClientByUserId(userId);
            var client = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            var training = await _groupTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            var _participationSpec = new FindParticipationByTrainingId(training.Id);
            var participations = await _groupTrainingParticipationRepository.ListAsync(_participationSpec);

            GroupTrainingParticipation participation = null;

            foreach (var part in participations)
            {
                if(part.ClientId == client.Id) participation = part;
            }

            if (participation == null)
            {
                return Result.Error("Nie znaleziono rezerwacji miejsca");
            }

            await _groupTrainingParticipationRepository.DeleteAsync(participation);

            return Result.Success();

        }

        public async Task<Result> CreateTraining(NewGroupTrainingModel model, string userId)
        {
            var _trainerSpec = new FindGroupTrainerByUserId(userId);
            var trainer = await _groupTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            if (model.IsCyclic)
            {
                var trainings = new List<GroupTraining>();
                DateTime startDate = model.Date;
                DateTime repeatUntilDate = new DateTime(year: 2025, month: 3, day: 31);

                if (model.Repeatability == "everyday")
                {
                    int totalDays = (repeatUntilDate - startDate).Days;

                    for (int i = 0; i <= totalDays; i++)
                    {
                        var training = new GroupTraining
                        {
                            GroupTrainerId = trainer.Id,
                            Date = startDate.AddDays(i),
                            MaxParticipantNumber = model.MaxParticipantNumber,
                            TrainingTypeId = model.TrainingTypeId,
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
                        var training = new GroupTraining
                        {
                            GroupTrainerId = trainer.Id,
                            Date = startDate.AddDays(i * 7),
                            MaxParticipantNumber = model.MaxParticipantNumber,
                            TrainingTypeId = model.TrainingTypeId,
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
                        var training = new GroupTraining
                        {
                            GroupTrainerId = trainer.Id,
                            Date = startDate.AddMonths(i),
                            MaxParticipantNumber = model.MaxParticipantNumber,
                            TrainingTypeId = model.TrainingTypeId,
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

                await _groupTrainingRepository.AddRangeAsync(trainings);
                return Result.Success();

            }
            else
            {
                var training = new GroupTraining
                {
                    GroupTrainerId = trainer.Id,
                    Date = model.Date,
                    MaxParticipantNumber = model.MaxParticipantNumber,
                    TrainingTypeId = model.TrainingTypeId,
                    Description = model.Description is null ? "" : model.Description,
                    Duration = model.Duration,
                };

                training.Date = training.Date.AddHours(model.Hour.Hour);

                await _groupTrainingRepository.AddAsync(training);
                return Result.Success();
            }
        }

        public async Task<Result> DeleteTraining(int trainingId, string userId)
        {
            var _trainerSpec = new FindGroupTrainerByUserId(userId);
            var trainer = await _groupTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var training = await _groupTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            await _groupTrainingRepository.DeleteAsync(training);

            return Result.Success();
        }

        public async Task<Result> ReservePlace(int trainingId, string userId)
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

            var training = await _groupTrainingRepository.GetByIdAsync(trainingId);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            var _participationSpec = new FindParticipationByTrainingId(training.Id);
            var participations = await _groupTrainingParticipationRepository.ListAsync(_participationSpec);

            training.Participations = participations;

            if (training.Participations.Count >= training.MaxParticipantNumber)
            {
                return Result.Error("Trening jest już pełny");
            }

            if (training.Participations.Any(p => p.ClientId == client.Id))
            {
                return Result.Error("Klient jest już zapisany na ten trening");
            }

            var groupTrainingParticipation = new GroupTrainingParticipation
            {
                ClientId = client.Id,
                GroupTrainingId = training.Id,
            };

            await _groupTrainingParticipationRepository.AddAsync(groupTrainingParticipation);

            return Result.Success();
            
        }

        public async Task<Result> UpdateTraining(EditGroupTrainingModel model, string userId)
        {
            var _trainerSpec = new FindGroupTrainerByUserId(userId);
            var trainer = await _groupTrainerRepository.FirstOrDefaultAsync(_trainerSpec);

            if (trainer == null)
            {
                return Result.Error("Nie znaleziono trenera");
            }

            var training = await _groupTrainingRepository.GetByIdAsync(model.Id);

            if (training == null)
            {
                return Result.Error("Nie znaleziono treningu");
            }

            training.Date = model.Date;
            training.Duration = model.Duration;
            training.MaxParticipantNumber = model.MaxParticipantNumber;
            training.TrainingTypeId = model.TrainingTypeId;
            training.Description = model.Description is null ? "" : model.Description;
            training.Date = training.Date.AddHours(model.Hour.Hour);

            await _groupTrainingRepository.UpdateAsync(training);
            return Result.Success();
        }
    }
}
