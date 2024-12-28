namespace Web.ViewModels.Calendar.Trainer.Trainings.Group
{
    public class TrainingsCalendarIndexGroupTrainingItemViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public GroupTrainingsTypeItemViewModel TrainingType { get; set; }
        public int ParticipantsNumber { get; set; }
        public int MaxParticipantNumber { get; set; }
        public bool IsFull { get; set; }
        public List<GroupTrainingsTypeItemViewModel> TrainingTypes { get; set; } = new List<GroupTrainingsTypeItemViewModel>();
    }
}
