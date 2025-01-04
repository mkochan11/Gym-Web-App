using ApplicationCore.Enums;
using System.ComponentModel.DataAnnotations;
using Web.Validation;

namespace Web.InputModels.Report
{
    public class NewEmployeeReportInputModel
    {
        [Required(ErrorMessage = "Nazwa raportu jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa raportu nie może być dłuższa niż 100 znaków.")]
        [Display(Name = "Nazwa raportu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data początkowa jest wymagana.")]
        [Display(Name = "Data początkowa")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "Data końcowa jest wymagana.")]
        [Display(Name = "Data końcowa")]
        [GreaterThanDate("FromDate", ErrorMessage = "Data końcowa musi być późniejsza niż początkowa")]
        public DateTime ToDate { get; set; }
        public int EmployeeId { get; set; }
        public Position Position { get; set; }

    }
}
