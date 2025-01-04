using System.ComponentModel.DataAnnotations;
using System.Web.WebPages;
using Web.Validation;

namespace Web.InputModels.Report
{
    public class NewReportInputModel : IValidatableObject
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
        public bool ClientsReport { get; set; }
        public bool BudgetReport { get; set; }
        public bool IndividualTrainingsReport { get; set; }
        public bool GroupTrainingsReport { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FromDate <= ToDate)
            {
                yield return new ValidationResult("Data końcowa musi być późniejsza niż początkowa.", new[] { "ToDate" });
            }
        }
    }
}
