using ApplicationCore.Entities;
using ApplicationCore.Entities.Abstract;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Web.Interfaces;
using Web.ViewModels.Calendar.Trainings;

namespace Web.Services
{
    public class TrainingsCalendarViewModelService : ITrainingsCalendarViewModelService
    {
        private readonly IRepository<IndividualTraining> _individualTrainingRepository;
        private readonly IRepository<GroupTraining> _groupTrainingRepository;
        private readonly IRepository<TrainingType> _trainingTypeRepository;
        private readonly IRepository<GroupTrainer> _groupTrainerRepository;
        private readonly IRepository<PersonalTrainer> _personalTrainerRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<GroupTrainingParticipation> _groupTrainingParticipationRepository;

        public TrainingsCalendarViewModelService( 
            IRepository<IndividualTraining> individualTrainingRepository,
            IRepository<GroupTraining> groupTrainingRepository,
            IRepository<TrainingType> trainingTypeRepository,
            IRepository<GroupTrainer> groupTrainerRepository,
            IRepository<PersonalTrainer> personalTrainerRepository,
            IRepository<Client> clientRepository,
            IRepository<GroupTrainingParticipation> groupTrainingParticipationRepository
            )
        {
            _individualTrainingRepository = individualTrainingRepository;
            _groupTrainingRepository = groupTrainingRepository;
            _trainingTypeRepository = trainingTypeRepository;
            _groupTrainerRepository = groupTrainerRepository;
            _personalTrainerRepository = personalTrainerRepository;
            _clientRepository = clientRepository;
            _groupTrainingParticipationRepository = groupTrainingParticipationRepository;
        }
        public async Task<TrainingsCalendarIndexViewModel> GetTrainingsCalendarIndexViewModel(int month, int year)
        {
            var _individualTrainingSpec = new FindIndividualTrainigByMonth(month, year);
            var individualTrainings = await _individualTrainingRepository.ListAsync(_individualTrainingSpec);

            var _groupTrainingSpec = new FindGroupTrainigByMonth(month, year);
            var groupTrainings = await _groupTrainingRepository.ListAsync(_groupTrainingSpec);

            var _individualTrainingItems = new List<TrainingsCalendarIndexIndividualTrainingItemViewModel>();
            var _groupTrainingItems = new List<TrainingsCalendarIndexGroupTrainingItemViewModel>();

            foreach (var item in individualTrainings)
            {

                item.PersonalTrainer = await _personalTrainerRepository.GetByIdAsync(item.PersonalTrainerId);

                if (item.ClientId != null)
                {
                    item.Client = await _clientRepository.GetByIdAsync(item.ClientId);
                }

                _individualTrainingItems.Add(new TrainingsCalendarIndexIndividualTrainingItemViewModel
                {
                    Id = item.Id,
                    Date = item.Date,
                    Duration = item.Duration,
                    Description = item.Description,
                    TrainerId = item.PersonalTrainerId,
                    TrainerName = item.PersonalTrainer is null ? "Nie znaleziono trenera" : item.PersonalTrainer.Name,
                    TrainerSurname = item.PersonalTrainer is null ? "" : item.PersonalTrainer.Surname,
                    IsReserved = item.Client is null ? false : true,
                });
            }

            foreach (var item in groupTrainings)
            {
                item.GroupTrainer = await _groupTrainerRepository.GetByIdAsync(item.GroupTrainerId);
                item.Participations = await _groupTrainingParticipationRepository.ListAsync();
                item.TrainingType = await _trainingTypeRepository.GetByIdAsync(item.TrainingTypeId);

                _groupTrainingItems.Add(new TrainingsCalendarIndexGroupTrainingItemViewModel
                {
                    Id = item.Id,
                    Date = item.Date,
                    Duration = item.Duration,
                    Description = item.Description,
                    TrainerId = item.GroupTrainerId,
                    TrainerName = item.GroupTrainer is null ? "Nie znaleziono trenera" : item.GroupTrainer.Name,
                    TrainerSurname = item.GroupTrainer is null ? "" : item.GroupTrainer.Surname,
                    MaxParticipantNumber = item.MaxParticipantNumber,
                    LiveParticipantNumber = item.Participations.Count,
                    IsFull = item.Participations.Count < item.MaxParticipantNumber ? false : true,
                    TrainingTypeId = item.TrainingTypeId,
                    TrainingName = item.TrainingType is null ? "Nie znaleziono typu treningu" : item.TrainingType.Name,
                    TrainingDescription = item.TrainingType is null ? "Nie znaleziono typu treningu" : item.TrainingType.Description,
                });
            }

            var daysInMonth = GetDaysInMonth(year, month);

            foreach (var day in daysInMonth)
            {
                if (day != null)
                {
                    foreach (var ind_training in _individualTrainingItems)
                    {
                        if (ind_training.Date.Day == day.Day)
                        {
                            day.IndividualTrainings.Add(ind_training);
                        }
                    }

                    day.IndividualTrainings = day.IndividualTrainings.OrderBy(ind => ind.Date.TimeOfDay).ToList();

                    foreach (var group_training in _groupTrainingItems)
                    {
                        if(group_training.Date.Day == day.Day)
                        {
                            day.GroupTrainings.Add(group_training);
                        }
                    }

                    day.GroupTrainings = day.GroupTrainings.OrderBy(ind => ind.Date.TimeOfDay).ToList();
                }
            }

            var viewModel = new TrainingsCalendarIndexViewModel
            {
                DaysInMonth = daysInMonth,
            };
            
            return viewModel;
        }

        private List<TrainingsCalendarIndexDayItemViewModel> GetDaysInMonth(int year, int month)
        {
            List<TrainingsCalendarIndexDayItemViewModel> days = new List<TrainingsCalendarIndexDayItemViewModel>();
            var firstDayOfMonth = new DateTime(year, month, 1);
            var daysInMonth = DateTime.DaysInMonth(year, month);

            // znalezienie dnia tygodnia pierwszego dnia miesiąca
            var startDay = (int)firstDayOfMonth.DayOfWeek;

            // dodanie pustych dni na początku miesiąca
            for (int i = 0; i < startDay; i++)
            {
                days.Add(null);
            }

            // dodanie dni w aktualnym miesiącu
            for (int i = 1; i <= daysInMonth; i++)
            {
                days.Add(new TrainingsCalendarIndexDayItemViewModel
                {
                    Day = i
                });
            }

            // Fill in the remaining empty days at the end (next month's days)
            int remainingDays = 42 - (startDay + daysInMonth); // 42 = 6 rows of 7 days
            for (int i = 0; i < remainingDays; i++)
            {
                days.Add(null); // Empty day
            }

            return days;
        }
    }
}

