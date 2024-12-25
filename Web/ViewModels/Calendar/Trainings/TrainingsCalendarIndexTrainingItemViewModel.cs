namespace Web.ViewModels.Calendar.Trainings
{
    public abstract class TrainingsCalendarIndexTrainingItemViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TrainerId { get; set; }
        public string TrainerName { get; set; } = string.Empty;
        public string TrainerSurname { get; set; } = string.Empty;
    }
}
