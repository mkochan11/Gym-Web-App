using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Web.Interfaces;
using Web.ViewModels.TrainingPlan.Add;

namespace Web.Services
{
    public class TrainingPlanViewModelService : ITrainingPlanViewModelService
    {
        private readonly IRepository<IndividualTraining> _individualTrainingRepository;
        private readonly IRepository<PersonalTrainer> _personalTrainerRepository;
        private readonly IRepository<Client> _clientRepository;

        public TrainingPlanViewModelService(
            IRepository<IndividualTraining> individualTrainingRepository,
            IRepository<PersonalTrainer> personalTrainerRepository,
            IRepository<Client> clientRepository)
        {
            _individualTrainingRepository = individualTrainingRepository;
            _personalTrainerRepository = personalTrainerRepository;
            _clientRepository = clientRepository;
        }

        public async Task<AddTrainingPlanIndexViewModel> GetAddTrainingPlanIndexViewModel(string userId)
        {
            var _personalTrainerSpec = new FindPersonalTrainerByUserId(userId);
            var personalTrainer = await _personalTrainerRepository.FirstOrDefaultAsync(_personalTrainerSpec);

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
    }
}
