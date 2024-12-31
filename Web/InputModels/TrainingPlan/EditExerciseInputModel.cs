using System.ComponentModel.DataAnnotations;

namespace Web.InputModels.TrainingPlan
{
    public class EditExerciseInputModel
    {
        [Required(ErrorMessage = "Nazwa ćwiczenia jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa ćwiczenia nie może być dłuższa niż 100 znaków.")]
        [Display(Name = "Nazwa ćwiczenia")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Opis ćwiczenia jest wymagany.")]
        [StringLength(500, ErrorMessage = "Opis ćwiczenia nie może być dłuższy niż 500 znaków.")]
        [Display(Name = "Opis ćwiczenia")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Liczba powtórzeń jest wymagana.")]
        [Range(1, 100, ErrorMessage = "Liczba powtórzeń musi być w zakresie od 1 do 100.")]
        [Display(Name = "Liczba powtórzeń")]
        public int RepetitionsNumber { get; set; }

        [Required(ErrorMessage = "Liczba serii jest wymagana.")]
        [Range(1, 100, ErrorMessage = "Liczba serii musi być w zakresie od 1 do 100.")]
        [Display(Name = "Liczba serii")]
        public int SeriesNumber { get; set; }

        [Required(ErrorMessage = "Czas odpoczynku jest wymagany.")]
        [Display(Name = "Czas odpoczynku")]
        public TimeSpan RestTime { get; set; }
    }
}
