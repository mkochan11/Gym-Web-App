using ApplicationCore.Entities.Abstract;
using Web.ViewModels.Owner.Employees;

namespace Web.Interfaces
{
    public interface IManageEmployeesViewModelService<T> where T : Employee
    {
        Task<ManageEmployeesIndexViewModel> GetManageEmployeesIndexViewModel();
    }
}
