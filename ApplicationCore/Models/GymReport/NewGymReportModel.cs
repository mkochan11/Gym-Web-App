namespace ApplicationCore.Models.GymReport
{
    public class NewGymReportModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool ClientsReport { get; set; }
        public bool BudgetReport { get; set; }
        public bool IndividualTrainingsReport { get; set; }
        public bool GroupTrainingsReport { get; set; }
    }
}
