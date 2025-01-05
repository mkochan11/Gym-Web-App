namespace Web.ViewModels.WorkSchedule.Receptionist
{
    public class ReceptionistWorkScheduleDayItemViewModel
    {
        public int? Day { get; set; }
        public DateTime? Date { get; set; }
        public List<ReceptionistWorkScheduleShiftItemViewModel> Shifts { get; set; } = new List<ReceptionistWorkScheduleShiftItemViewModel>();
    }
}
