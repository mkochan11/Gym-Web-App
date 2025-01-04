using Web.ViewModels.Manager.Reports;
using Web.ViewModels.Owner.Reports;

namespace Web.Interfaces
{
    public interface IReportsViewModelService
    {
        Task<OwnerReportsIndexViewModel> GetOwnerReportsIndexViewModel(string userId);
        Task<ManagerReportsIndexViewModel> GetManagerReportsIndexViewModel(string userId);
    }
}
