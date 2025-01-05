namespace Web.ViewModels.WorkSchedule.Manager
{
    public class ManagerWorkScheduleDayItemViewModel
    {
        public int? Day { get; set; }
        public DateTime? Date { get; set; }
        public List<ManagerWorkScheduleShiftItemViewModel> Shifts { get; set; } = new List<ManagerWorkScheduleShiftItemViewModel>();
    }
}
