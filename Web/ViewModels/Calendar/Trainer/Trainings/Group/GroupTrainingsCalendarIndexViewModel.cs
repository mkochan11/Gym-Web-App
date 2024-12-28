namespace Web.ViewModels.Calendar.Trainer.Trainings.Group
{
    public class GroupTrainingsCalendarIndexViewModel
    {
        public List<GroupTrainingsCalendarIndexDayItemViewModel> DaysInMonth { get; set; } = new List<GroupTrainingsCalendarIndexDayItemViewModel>();
        public List<GroupTrainingsTypeItemViewModel> TrainingTypes { get; set; } = new List<GroupTrainingsTypeItemViewModel>();
    }
}
