using System.ComponentModel.DataAnnotations;

namespace Web.InputModels.Shift
{
    public class NewShiftInputModel
    {
        [Required]
        [Display(Name = "Data")]
        public required DateTime Date { get; set; }

        [Required]
        [Display(Name = "Godzina rozpoczęcia")]
        public required DateTime StartHour { get; set; }
        [Required]
        [Display(Name = "Godzina zakończenia")]
        public required DateTime EndHour { get; set; }
        [Required]
        [Display(Name = "Recepcjonista")]
        public int ReceptionistId { get; set; }
    }
}
