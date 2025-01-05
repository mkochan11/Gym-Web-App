using Web.ViewModels.ManageClient;

namespace Web.Interfaces
{
    public interface IManageClientViewModelService
    {
        Task<ManageClientIndexViewModel> GetManageClientIndexViewModel(string clientId);
    }
}
