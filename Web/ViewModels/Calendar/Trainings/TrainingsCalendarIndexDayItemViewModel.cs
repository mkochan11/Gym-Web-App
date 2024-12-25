using ApplicationCore.Entities.Abstract;

namespace Web.ViewModels.Calendar.Trainings
{
    public class TrainingsCalendarIndexDayItemViewModel
    {
        public int? Day { get; set; }
        public List<TrainingsCalendarIndexIndividualTrainingItemViewModel> IndividualTrainings { get; set; } = new List<TrainingsCalendarIndexIndividualTrainingItemViewModel>();
        public List<TrainingsCalendarIndexGroupTrainingItemViewModel> GroupTrainings { get; set; } = new List<TrainingsCalendarIndexGroupTrainingItemViewModel>();
    }
}
