namespace Web.ViewModels.Calendar.Trainer.Trainings.Personal
{
    public class TrainingsCalendarIndexIndividualTrainingItemViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TrainerId { get; set; }
        public bool IsReserved { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientSurname { get; set; } = string.Empty;

    }
}
