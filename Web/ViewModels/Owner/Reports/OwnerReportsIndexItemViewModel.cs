namespace Web.ViewModels.Owner.Reports
{
    public class OwnerReportsIndexItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool BudgetReport { get; set; }
        public bool ClientsReport { get; set; }
        public bool IndividualTrainingsReport { get; set; }
        public bool GroupTrainingsReport { get; set; }
        public int? NewClients { get; set; }
        public int? NewMemberships { get; set; }
        public decimal? TotalIncome { get; set; }
        public decimal? TotalEmployeesCost { get; set; }
        public decimal? TotalProfit { get; set; }
        public int? TotalIndividualTrainings { get; set; }
        public TimeSpan? TotalIndividualTrainingsTime { get; set; }
        public int? TotalGroupTrainings { get; set; }
        public TimeSpan? TotalGroupTrainingsTime { get; set; }
    }
}
