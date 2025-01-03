using ApplicationCore.Entities.Abstract;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Employee;
using ApplicationCore.Models.User;
using Ardalis.Result;

namespace ApplicationCore.Services
{
    public class EmployeeService<T> : IEmployeeService<T> where T : Employee
    {
        private readonly IRepository<T> _employeeRepository;
        private readonly Func<T> _employeeFactory;

        public EmployeeService(IRepository<T> employeeRepository, Func<T> employeeFactory)
        {
            _employeeRepository = employeeRepository;
            _employeeFactory = employeeFactory;
        }

        public async Task<Result> AddEmployee(NewEmployeeModel model)
        {
            var employee = _employeeFactory();

            employee.AccountId = model.AccountId;
            employee.Name = model.Name;
            employee.Surname = model.Surname;
            employee.Position = model.Position;
            employee.RegistrationDate = DateTime.Now;


            var result = await _employeeRepository.AddAsync(employee);

            if (result == null)
            {
                return Result.Error("Wystąpił błąd podczas dodawania użytkownika.");
            }
            else
            {
                return Result.Success();
            }
        }

        public async Task<Result> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee == null)
            {
                return Result.Error("Nie znaleziono pracownika.");
            }

            await _employeeRepository.DeleteAsync(employee);
            return Result.Success();
        }

        public async Task<string> GetEmployeeAccountId(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                return "";
            }

            return employee.AccountId;
        }

        public async Task<Result> UpdateEmployee(EditEmployeeModel model)
        {
            var employee = await _employeeRepository.GetByIdAsync(model.Id);

            if (employee == null)
            {
                return Result.Error("Nie znaleziono pracownika.");
            }

            employee.Name = model.Name;
            employee.Surname = model.Surname;
            employee.Position = model.Position;

            await _employeeRepository.UpdateAsync(employee);
            return Result.Success();
        }
    }
}
