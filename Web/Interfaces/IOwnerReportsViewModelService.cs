using Web.ViewModels.Owner.Reports;

namespace Web.Interfaces
{
    public interface IOwnerReportsViewModelService
    {
        Task<OwnerReportsIndexViewModel> GetOwnerReportsIndexViewModel(string userId);
    }
}
