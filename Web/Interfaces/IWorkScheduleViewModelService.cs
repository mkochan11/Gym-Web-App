using Web.ViewModels.WorkSchedule.Manager;

namespace Web.Interfaces
{
    public interface IWorkScheduleViewModelService
    {
        Task<ManagerWorkScheduleIndexViewModel> GetManagerWorkScheduleIndexViewModel(int currentMonth, int currentYear);
    }
}
