
namespace ApplicationCore.Models.Shift
{
    public class EditShiftModel
    {
        public int ShiftId { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
        public int ReceptionistId { get; set; }
    }
}
