using System.ComponentModel.DataAnnotations;

namespace Web.InputModels.Shift
{
    public class EditShiftInputModel
    {
        public int Id { get; set; }
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
