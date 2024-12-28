namespace Web.ViewModels.Calendar.Trainer.Trainings.Group
{
    public class GroupTrainingsCalendarIndexDayItemViewModel
    {
        public int? Day { get; set; }
        public DateTime? Date { get; set; }
        public List<TrainingsCalendarIndexGroupTrainingItemViewModel> Trainings { get; set; } = new List<TrainingsCalendarIndexGroupTrainingItemViewModel>();
    }
}
