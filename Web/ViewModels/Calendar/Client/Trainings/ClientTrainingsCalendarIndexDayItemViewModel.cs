using ApplicationCore.Entities.Abstract;

namespace Web.ViewModels.Calendar.Client.Trainings
{
    public class ClientTrainingsCalendarIndexDayItemViewModel
    {
        public int? Day { get; set; }
        public List<ClientTrainingsCalendarIndexIndividualTrainingItemViewModel> IndividualTrainings { get; set; } = new List<ClientTrainingsCalendarIndexIndividualTrainingItemViewModel>();
        public List<ClientTrainingsCalendarIndexGroupTrainingItemViewModel> GroupTrainings { get; set; } = new List<ClientTrainingsCalendarIndexGroupTrainingItemViewModel>();
    }
}
