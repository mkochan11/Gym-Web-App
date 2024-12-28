using ApplicationCore.Entities;
using ApplicationCore.Entities.Abstract;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.Result;
using Web.Interfaces;
using Web.ViewModels.Calendar.Client.Trainings;
using Web.ViewModels.Calendar.Trainer.Trainings.Group;
using Web.ViewModels.Calendar.Trainer.Trainings.Personal;

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
        public async Task<ClientTrainingsCalendarIndexViewModel> GetClientTrainingsCalendarIndexViewModel(int month, int year, string userId)
        {
            var _clientSpec = new FindClientByUserId(userId);
            var user = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            var _individualTrainingSpec = new FindIndividualTrainingByMonth(month, year);
            var individualTrainings = await _individualTrainingRepository.ListAsync(_individualTrainingSpec);

            var _groupTrainingSpec = new FindGroupTrainingByMonth(month, year);
            var groupTrainings = await _groupTrainingRepository.ListAsync(_groupTrainingSpec);

            var _individualTrainingItems = new List<ClientTrainingsCalendarIndexIndividualTrainingItemViewModel>();
            var _groupTrainingItems = new List<ClientTrainingsCalendarIndexGroupTrainingItemViewModel>();

            foreach (var item in individualTrainings)
            {
                item.PersonalTrainer = await _personalTrainerRepository.GetByIdAsync(item.PersonalTrainerId);

                if (item.ClientId != null)
                {
                    item.Client = await _clientRepository.GetByIdAsync(item.ClientId);
                }

                _individualTrainingItems.Add(new ClientTrainingsCalendarIndexIndividualTrainingItemViewModel
                {
                    Id = item.Id,
                    Date = item.Date,
                    Duration = item.Duration,
                    Description = item.Description,
                    TrainerId = item.PersonalTrainerId,
                    TrainerName = item.PersonalTrainer is null ? "Nie znaleziono trenera" : item.PersonalTrainer.Name,
                    TrainerSurname = item.PersonalTrainer is null ? "" : item.PersonalTrainer.Surname,
                    IsReserved = item.Client is null ? false : true,
                    IsReservedByUser = item.Client == user ? true : false,
                });
            }

            foreach (var item in groupTrainings)
            {
                item.GroupTrainer = await _groupTrainerRepository.GetByIdAsync(item.GroupTrainerId);

                var _participationTrainingSpec = new FindParticipationByTrainingId(item.Id);
                item.Participations = await _groupTrainingParticipationRepository.ListAsync(_participationTrainingSpec);
                item.TrainingType = await _trainingTypeRepository.GetByIdAsync(item.TrainingTypeId);

                var isReservedByUser = false;
                if(item.Participations != null)
                {
                    foreach(var participation in item.Participations)
                    {
                        if(participation.ClientId == user.Id) isReservedByUser = true;
                    }
                }

                _groupTrainingItems.Add(new ClientTrainingsCalendarIndexGroupTrainingItemViewModel
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
                    IsReservedByUser = isReservedByUser,
                });
            }

            var daysInMonth = GetClientDaysInMonth(year, month);

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

            var viewModel = new ClientTrainingsCalendarIndexViewModel
            {
                DaysInMonth = daysInMonth,
            };
            
            return viewModel;
        }

        public async Task<PersonalTrainingsCalendarIndexViewModel> GetPersonalTrainingsCalendarIndexViewModel(int month, int year, string userId)
        {
            var _personalTrainerSpec = new FindPersonalTrainerByUserId(userId);
            var personalTrainer = await _personalTrainerRepository.FirstOrDefaultAsync(_personalTrainerSpec);

            var _individualTrainingSpec = new FindIndividualTrainingByMonth(month, year);
            var individualTrainings = await _individualTrainingRepository.ListAsync(_individualTrainingSpec);

            var trainersTrainings = individualTrainings
                .Where(training => training.PersonalTrainerId == personalTrainer.Id)
                .ToList();

            var _individualTrainingItems = new List<TrainingsCalendarIndexIndividualTrainingItemViewModel>();

            foreach (var item in trainersTrainings)
            {

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
                    IsReserved = item.Client is null ? false : true,
                    ClientName = item.Client is null ? "" : item.Client.Name,
                    ClientSurname = item.Client is null ? "" : item.Client.Surname
                });
            }

            var daysInMonth = GetPersonalTrainerDaysInMonth(year, month);

            foreach (var day in daysInMonth)
            {
                if (day != null)
                {
                    foreach (var ind_training in _individualTrainingItems)
                    {
                        if (ind_training.Date.Day == day.Day)
                        {
                            day.Trainings.Add(ind_training);
                        }
                    }

                    day.Trainings = day.Trainings.OrderBy(ind => ind.Date.TimeOfDay).ToList();
                }
            }

            var viewModel = new PersonalTrainingsCalendarIndexViewModel
            {
                DaysInMonth = daysInMonth,
            };

            return viewModel;
        }

        public async Task<GroupTrainingsCalendarIndexViewModel> GetGroupTrainingsCalendarIndexViewModel(int month, int year, string userId)
        {
            var _groupTrainerSpec = new FindGroupTrainerByUserId(userId);
            var groupTrainer = await _groupTrainerRepository.FirstOrDefaultAsync(_groupTrainerSpec);

            var _groupTrainingSpec = new FindGroupTrainingByMonth(month, year);
            var groupTrainings = await _groupTrainingRepository.ListAsync(_groupTrainingSpec);

            var trainersTrainings = groupTrainings
                .Where(training => training.GroupTrainerId == groupTrainer.Id)
                .ToList();

            var trainingTypes = await _trainingTypeRepository.ListAsync();

            var _trainingTypeItems = new List<GroupTrainingsTypeItemViewModel>();

            foreach (var trainingType in trainingTypes)
            {
                _trainingTypeItems.Add(new GroupTrainingsTypeItemViewModel
                {
                    Id = trainingType.Id,
                    Name = trainingType.Name,
                    Description = trainingType.Description,
                });
            }

            var _groupTrainingItems = new List<TrainingsCalendarIndexGroupTrainingItemViewModel>();

            foreach (var item in trainersTrainings)
            {
                var _participationSpec = new FindParticipationByTrainingId(item.Id);
                var participations = await _groupTrainingParticipationRepository.ListAsync(_participationSpec);

                var trainingType = _trainingTypeItems
                    .Where(type => type.Id == item.TrainingTypeId)
                    .FirstOrDefault();

                _groupTrainingItems.Add(new TrainingsCalendarIndexGroupTrainingItemViewModel
                {
                    Id = item.Id,
                    Date = item.Date,
                    Duration = item.Duration,
                    Description = item.Description,
                    TrainingType = trainingType is null ? new GroupTrainingsTypeItemViewModel() : trainingType,
                    MaxParticipantNumber = item.MaxParticipantNumber,
                    ParticipantsNumber = participations.Count,
                    IsFull = item.MaxParticipantNumber == participations.Count ? true : false,
                    TrainingTypes = _trainingTypeItems
                });
            }

            var daysInMonth = GetGroupTrainerDaysInMonth(year, month);

            foreach (var day in daysInMonth)
            {
                if (day != null)
                {
                    foreach (var group_training in _groupTrainingItems)
                    {
                        if (group_training.Date.Day == day.Day)
                        {
                            day.Trainings.Add(group_training);
                        }
                    }

                    day.Trainings = day.Trainings.OrderBy(ind => ind.Date.TimeOfDay).ToList();
                }
            }

            var viewModel = new GroupTrainingsCalendarIndexViewModel
            {
                DaysInMonth = daysInMonth,
                TrainingTypes = _trainingTypeItems
            };

            return viewModel;
        }

        public async Task<List<GroupTrainingsTypeItemViewModel>> GetGroupTrainingsTypeItemViewModels()
        {
            var trainingTypes = await _trainingTypeRepository.ListAsync();

            var _trainingTypeItems = new List<GroupTrainingsTypeItemViewModel>();

            foreach (var trainingType in trainingTypes)
            {
                _trainingTypeItems.Add(new GroupTrainingsTypeItemViewModel
                {
                    Id = trainingType.Id,
                    Name = trainingType.Name,
                    Description = trainingType.Description,
                });
            }

            return _trainingTypeItems;
        }


        private List<ClientTrainingsCalendarIndexDayItemViewModel> GetClientDaysInMonth(int year, int month)
        {
            List<ClientTrainingsCalendarIndexDayItemViewModel> days = new List<ClientTrainingsCalendarIndexDayItemViewModel>();
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
                days.Add(new ClientTrainingsCalendarIndexDayItemViewModel
                {
                    Day = i,
                    Date = new DateTime(year, month, i)
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

        private List<PersonalTrainingsCalendarIndexDayItemViewModel> GetPersonalTrainerDaysInMonth(int year, int month)
        {
            List<PersonalTrainingsCalendarIndexDayItemViewModel> days = new List<PersonalTrainingsCalendarIndexDayItemViewModel>();
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
                days.Add(new PersonalTrainingsCalendarIndexDayItemViewModel
                {
                    Day = i,
                    Date = new DateTime(year, month, i)
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

        private List<GroupTrainingsCalendarIndexDayItemViewModel> GetGroupTrainerDaysInMonth(int year, int month)
        {
            List<GroupTrainingsCalendarIndexDayItemViewModel> days = new List<GroupTrainingsCalendarIndexDayItemViewModel>();
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
                days.Add(new GroupTrainingsCalendarIndexDayItemViewModel
                {
                    Day = i,
                    Date = new DateTime(year, month, i)
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

