namespace Web.ViewModels.Owner.Reports
{
    public class OwnerReportsIndexItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? NewClients { get; set; }
        public int? NewMemberships { get; set; }
        public decimal? TotalIncome { get; set; }
        public int? TotalIndividualTrainings { get; set; }
        public int? TotalGroupTrainings { get; set; }
        public TimeSpan? TotalIndividualTrainingsTime { get; set; }
        public TimeSpan? TotalGroupTrainingsTime { get; set; }
        public decimal? TotalEmployeesCost { get; set; }
    }
}
