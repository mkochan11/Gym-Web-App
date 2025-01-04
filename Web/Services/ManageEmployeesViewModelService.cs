using ApplicationCore.Entities.Abstract;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Web.Interfaces;
using Web.ViewModels.Owner.Employees;

namespace Web.Services
{
    public class ManageEmployeesViewModelService<T> : IManageEmployeesViewModelService<T> where T : Employee
    {
        private readonly IRepository<T> _employeesRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageEmployeesViewModelService(IRepository<T> employeesRepository, UserManager<ApplicationUser> userManager)
        {
            _employeesRepository = employeesRepository;
            _userManager = userManager;
        }
        public async Task<ManageEmployeesIndexViewModel> GetManageEmployeesIndexViewModel()
        {
            var users = await _employeesRepository.ListAsync();

            var userItems = new List<ManageEmployeesIndexItemViewModel>();

            foreach (var user in users)
            {
                var appUser = await _userManager.FindByIdAsync(user.AccountId);

                userItems.Add(new ManageEmployeesIndexItemViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Salary = user.Salary,
                    Email = appUser is null ? "Nie znaleziono adresu e-mail" : appUser.Email,
                    EmploymentDate = user.EmploymentDate,
                });
            }

            var viewModel = new ManageEmployeesIndexViewModel
            {
                Employees = userItems,
            };
            return viewModel;
        }
    }
}
