namespace ApplicationCore.Models.Shift
{
    public class NewShiftModel
    {
        public DateTime Date { get; set; }
        public required DateTime StartHour { get; set; }
        public required DateTime EndHour { get; set; }
        public int ReceptionistId { get; set; }
    }
}
