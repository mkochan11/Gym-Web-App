using Web.ViewModels.WorkSchedule.Manager;
using Web.ViewModels.WorkSchedule.Receptionist;

namespace Web.Interfaces
{
    public interface IWorkScheduleViewModelService
    {
        Task<ManagerWorkScheduleIndexViewModel> GetManagerWorkScheduleIndexViewModel(int currentMonth, int currentYear);
        Task<ReceptionistWorkScheduleIndexViewModel> GetReceptionistWorkScheduleIndexViewModel(int currentMonth, int currentYear, string userId);
    }
}
