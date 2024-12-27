using System.ComponentModel.DataAnnotations;

namespace Web.InputModels.Calendar.Trainer.Trainings.Personal
{
    public class NewIndividualTrainingInputModel
    {
        [Required]
        [Display(Name = "Data")]
        public required DateTime Date { get; set; }

        [Required]
        [Display(Name = "Godzina")]
        public required DateTime Hour { get; set; }

        [Required]
        [Display(Name = "Czas trwania")]
        public required TimeSpan Duration { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Czy cyklicznie?")]
        public required bool IsCyclic { get; set; }

        public string? Repeatability { get; set; }
    }
}
