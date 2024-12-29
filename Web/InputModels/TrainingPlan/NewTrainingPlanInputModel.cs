using System.ComponentModel.DataAnnotations;

namespace Web.InputModels.TrainingPlan
{
    public class NewTrainingPlanInputModel
    {
        [Required(ErrorMessage = "Nazwa planu treningowego jest wymagana.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nazwa planu treningowego powinna mieć od 3 do 100 znaków.")]
        [Display(Name = "Nazwa planu treningowego")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Opis planu treningowego jest wymagany.")]
        [StringLength(500, ErrorMessage = "Opis planu treningowego nie może być dłuższy niż 500 znaków.")]
        [Display(Name = "Opis planu treningowego")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Klient jest wymagany.")]
        [Range(1, int.MaxValue, ErrorMessage = "Proszę wybrać prawidłowego klienta.")]
        [Display(Name = "Klient")]
        public int ClientId { get; set; }
    }
}
