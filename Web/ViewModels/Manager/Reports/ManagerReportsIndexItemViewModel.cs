namespace Web.ViewModels.Manager.Reports
{
    public class ManagerReportsIndexItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string EmployeeName { get; set; }
        public ManagerReportsDetailedItemViewModel DetailedReport { get; set; }
    }
}
