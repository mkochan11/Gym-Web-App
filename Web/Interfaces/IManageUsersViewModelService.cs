using ApplicationCore.Entities.Abstract;
using Web.ViewModels.Admin.ManageUsers;

namespace Web.Interfaces
{
    public interface IManageUsersViewModelService<T> where T : User
    {
        Task<ManageUsersIndexViewModel> GetManageUsersIndexViewModel();
    }
}
