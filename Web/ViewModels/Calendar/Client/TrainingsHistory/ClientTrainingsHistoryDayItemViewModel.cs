namespace Web.ViewModels.Calendar.Client.TrainingsHistory
{
    public class ClientTrainingsHistoryDayItemViewModel
    {
        public int? Day { get; set; }
        public DateTime Date { get; set; }
        public List<ClientTrainingsHistoryGroupTrainingItemViewModel> GroupTrainings { get; set; } = new List<ClientTrainingsHistoryGroupTrainingItemViewModel>();
        public List<ClientTrainingsHistoryIndividualTrainingItemViewModel> IndividualTrainings { get; set; } = new List<ClientTrainingsHistoryIndividualTrainingItemViewModel>();
    }
}
