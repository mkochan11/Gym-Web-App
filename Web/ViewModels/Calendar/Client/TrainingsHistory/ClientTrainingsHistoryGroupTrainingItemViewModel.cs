namespace Web.ViewModels.Calendar.Client.TrainingsHistory
{
    public class ClientTrainingsHistoryGroupTrainingItemViewModel : ClientTrainingsHistoryTrainingItemViewModel
    {
        public int TrainingTypeId { get; set; }
        public string TrainingName { get; set; } = string.Empty;
        public string TrainingDescription { get; set; } = string.Empty;
    }
}
