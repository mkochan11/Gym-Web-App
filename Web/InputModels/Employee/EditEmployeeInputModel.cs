using System.ComponentModel.DataAnnotations;

namespace Web.InputModels.Employee
{
    public class EditEmployeeInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane.")]
        [StringLength(50, ErrorMessage = "Imię musi mieć od 2 do 50 znaków.", MinimumLength = 2)]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(50, ErrorMessage = "Nazwisko musi mieć od 2 do 50 znaków.", MinimumLength = 2)]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail.")]
        [Display(Name = "Adres e-mail")]
        public string Email { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Wynagrodzenie musi być liczbą dodatnią.")]
        [Display(Name = "Wynagrodzenie")]
        public decimal? Salary { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data zatrudnienia")]
        public DateTime? EmploymentDate { get; set; }
    }
}
