namespace Web.ViewModels.WorkSchedule.Manager
{
    public class ManagerWorkScheduleIndexViewModel
    {
        public List<ManagerWorkScheduleDayItemViewModel> DaysInMonth { get; set; } = new List<ManagerWorkScheduleDayItemViewModel>();
        public List<ManagerWorkScheduleReceptionistItemViewModelService> Receptionists { get; set; } = new List<ManagerWorkScheduleReceptionistItemViewModelService>();
    }
}
