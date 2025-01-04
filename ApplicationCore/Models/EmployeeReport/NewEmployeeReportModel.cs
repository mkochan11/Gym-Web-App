using ApplicationCore.Enums;

namespace ApplicationCore.Models.EmployeeReport
{
    public class NewEmployeeReportModel
    {
        public string ManagerId { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int EmployeeId { get; set; }
        public Position Position { get; set; }
    }
}
