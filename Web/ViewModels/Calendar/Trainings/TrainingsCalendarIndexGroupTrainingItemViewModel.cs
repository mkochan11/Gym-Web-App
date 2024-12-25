namespace Web.ViewModels.Calendar.Trainings
{
    public class TrainingsCalendarIndexGroupTrainingItemViewModel : TrainingsCalendarIndexTrainingItemViewModel
    {
        public int MaxParticipantNumber { get; set; }
        public int LiveParticipantNumber { get; set; }
        public bool IsFull { get; set; }
        public int TrainingTypeId {  get; set; }
        public string TrainingName { get; set; } = string.Empty;
        public string TrainingDescription { get; set; } = string.Empty;
    }
}
