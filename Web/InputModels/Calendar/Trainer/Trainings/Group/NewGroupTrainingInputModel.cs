using System.ComponentModel.DataAnnotations;

namespace Web.InputModels.Calendar.Trainer.Trainings.Group
{
    public class NewGroupTrainingInputModel
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
        public string? Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Typ treningu")]
        public int TrainingTypeId { get; set; }

        [Required]
        [Display(Name = "Ilu uczestników")]
        [Range(1, 100, ErrorMessage = "Liczba uczestników musi być między 1 a 100.")]
        public int MaxParticipantNumber { get; set; }

        [Required]
        [Display(Name = "Czy cyklicznie?")]
        public required bool IsCyclic { get; set; }

        public string? Repeatability { get; set; }
    }
}
