using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Web.Interfaces;
using Web.ViewModels.TrainingPlan.Add;
using Web.ViewModels.TrainingPlan.Edit;
using Web.ViewModels.TrainingPlan.Index;

namespace Web.Services
{
    public class TrainingPlanViewModelService : ITrainingPlanViewModelService
    {
        private readonly IRepository<IndividualTraining> _individualTrainingRepository;
        private readonly IRepository<PersonalTrainer> _personalTrainerRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<TrainingPlan> _trainingPlanRepository;
        private readonly IRepository<Exercise> _exerciseRepository;

        public TrainingPlanViewModelService(
            IRepository<IndividualTraining> individualTrainingRepository,
            IRepository<PersonalTrainer> personalTrainerRepository,
            IRepository<Client> clientRepository,
            IRepository<TrainingPlan> trainingPlanRepository,
            IRepository<Exercise> exerciseRepository)
        {
            _individualTrainingRepository = individualTrainingRepository;
            _personalTrainerRepository = personalTrainerRepository;
            _clientRepository = clientRepository;
            _trainingPlanRepository = trainingPlanRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<AddTrainingPlanIndexViewModel> GetAddTrainingPlanIndexViewModel(string userId)
        {
            var _personalTrainerSpec = new FindPersonalTrainerByUserId(userId);
            var personalTrainer = await _personalTrainerRepository.FirstOrDefaultAsync(_personalTrainerSpec);

            var clients = await GetTrainersClients(personalTrainer);

            var _clientItems = new List<AddTrainingPlanClientItemViewModel>();

            foreach (var client in clients)
            {
                _clientItems.Add(new AddTrainingPlanClientItemViewModel
                {
                    Id = client.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                });
            }

            var viewModel = new AddTrainingPlanIndexViewModel
            {
                ClientItems = _clientItems,
            };

            return viewModel;
        }

        public async Task<EditTrainingPlanIndexViewModel> GetEditTrainingPlanIndexViewModel(int planId, string userId)
        {
            var _personalTrainerSpec = new FindPersonalTrainerByUserId(userId);
            var personalTrainer = await _personalTrainerRepository.FirstOrDefaultAsync(_personalTrainerSpec);

            var clients = await GetTrainersClients(personalTrainer);

            var _clientItems = new List<EditTrainingPlanClientItemViewModel>();

            foreach (var client in clients)
            {
                _clientItems.Add(new EditTrainingPlanClientItemViewModel
                {
                    Id = client.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                });
            }

            var trainingPlan = await _trainingPlanRepository.GetByIdAsync(planId);

            var _exerciseSpec = new FindExerciseByTrainingPlanId(planId);
            var exercises = await _exerciseRepository.ListAsync(_exerciseSpec);

            var _exerciseItems = new List<EditTrainingPlanExerciseItemViewModel>();

            if (exercises.Any())
            {
                foreach (var exercise in exercises)
                {
                    _exerciseItems.Add(new EditTrainingPlanExerciseItemViewModel
                    {
                        Id = exercise.Id,
                        Name = exercise.Name,
                        Description = exercise.Description,
                        RepetitionsNumber = exercise.RepetitionsNumber,
                        SeriesNumber = exercise.SeriesNumber,
                        RestTime = exercise.RestTime,
                        EditExerciseInputModel = new InputModels.TrainingPlan.EditExerciseInputModel
                        {
                            Name = exercise.Name,
                            Description = exercise.Description,
                            RepetitionsNumber = exercise.RepetitionsNumber,
                            SeriesNumber = exercise.SeriesNumber,
                            RestTime = exercise.RestTime,
                        }
                    });
                }
            }

            var _trainingPlanItem = new EditTrainingPlanItemViewModel
            {
                Id = trainingPlan.Id,
                Name = trainingPlan.Name,
                Description = trainingPlan.Description,
                ExerciseItems = _exerciseItems,
                ClientId = trainingPlan.ClientId
            };

            var viewModel = new EditTrainingPlanIndexViewModel
            {
                ClientItems = _clientItems,
                TrainingPlanItem = _trainingPlanItem,
            };

            return viewModel;
        }

        public async Task<TrainingPlansIndexViewModel> GetTrainingPlansIndexViewModel(string userId)
        {
            var _personalTrainerSpec = new FindPersonalTrainerByUserId(userId);
            var personalTrainer = await _personalTrainerRepository.FirstOrDefaultAsync(_personalTrainerSpec);

            var _trainingPlanSpec = new FindTrainingPlanByTrainerId(personalTrainer.Id);
            var trainingPlans = await _trainingPlanRepository.ListAsync(_trainingPlanSpec);

            var trainingPlanItems = new List<TrainingPlansIndexItemViewModel>();

            foreach (var trainingPlan in trainingPlans)
            {
                var planItem = new TrainingPlansIndexItemViewModel
                {
                    Id = trainingPlan.Id,
                    Name = trainingPlan.Name,
                    Description = trainingPlan.Description,
                };

                trainingPlanItems.Add(planItem);
            }

            var viewModel = new TrainingPlansIndexViewModel
            {
                TrainingPlans = trainingPlanItems,
            };

            return viewModel;
        }

        private async Task<List<Client>> GetTrainersClients(PersonalTrainer personalTrainer)
        {
            var individualTrainings = await _individualTrainingRepository.ListAsync();

            var trainersTrainings = individualTrainings
                .Where(training => training.PersonalTrainerId == personalTrainer.Id)
                .ToList();

            var pastTrainings = trainersTrainings
                .Where(training => training.Date < DateTime.Now)
                .ToList();

            var clients = new List<Client>();
            if (pastTrainings.Any())
            {
                foreach (var training in pastTrainings)
                {
                    var client = training.ClientId is null ? null : await _clientRepository.GetByIdAsync(training.ClientId);
                    if (client != null && !clients.Contains(client))
                    {
                        clients.Add(client);
                    }
                }
            }

            return clients;
        }
    }
}
