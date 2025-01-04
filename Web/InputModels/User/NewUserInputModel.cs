using System.ComponentModel.DataAnnotations;

namespace Web.InputModels.User
{
    public class NewUserInputModel
    {
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
    }
}
