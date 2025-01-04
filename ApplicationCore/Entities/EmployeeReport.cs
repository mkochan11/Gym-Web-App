using ApplicationCore.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class EmployeeReport : BaseEntity
    {
        public int ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public Manager Manager { get; set; }
        public int EmployeeId { get; set; }
        public Position Position { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalHoursWorked { get; set; }
        public decimal TotalSalary { get; set; }
    }
}
