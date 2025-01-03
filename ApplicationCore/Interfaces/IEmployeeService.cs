using ApplicationCore.Entities.Abstract;
using ApplicationCore.Models.Employee;
using ApplicationCore.Models.User;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IEmployeeService<T> where T : Employee
    {
        Task<Result> DeleteEmployee(int id);
        Task<Result> UpdateEmployee(EditEmployeeModel model);
        Task<Result> AddEmployee(NewEmployeeModel model);
        Task<string> GetEmployeeAccountId(int id);
    }
}
