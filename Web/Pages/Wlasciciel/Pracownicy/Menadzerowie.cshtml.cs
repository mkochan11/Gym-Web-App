using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Employee;
using ApplicationCore.Models.User;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.InputModels.Employee;
using Web.Interfaces;
using Web.ViewModels.Owner.Employees;

namespace Web.Pages.Wlasciciel.Pracownicy
{
    [Authorize(Roles = "Owner")]
    public class MenadzerowieModel : PageModel
    {
        IManageEmployeesViewModelService<Manager> _manageEmployeesViewModelService;
        private readonly IEmployeeService<Manager> _employeeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public MenadzerowieModel(IManageEmployeesViewModelService<Manager> manageEmplyeesViewModelService,
            IEmployeeService<Manager> employeeService,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore)
        {
            _manageEmployeesViewModelService = manageEmplyeesViewModelService;
            _employeeService = employeeService;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }

        public required ManageEmployeesIndexViewModel ViewModel { get; set; }

        [BindProperty]
        public NewEmployeeInputModel NewEmployeeInputModel { get; set; }

        [BindProperty]
        public EditEmployeeInputModel EditEmployeeInputModel { get; set; }

        public async Task OnGet()
        {
            ViewModel = await _manageEmployeesViewModelService.GetManageEmployeesIndexViewModel();
        }

        public async Task<IActionResult> OnPostCreate()
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, NewEmployeeInputModel.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, NewEmployeeInputModel.Email, CancellationToken.None);
            var registerResult = await _userManager.CreateAsync(user, AuthorizationConstants.DEFAULT_MANAGER_PASSWORD);

            if (!registerResult.Succeeded)
            {
                return RedirectToPage("/Error", new { errorMessage = registerResult.Errors.FirstOrDefault().Description });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Manager");
            if (!roleResult.Succeeded)
            {
                return RedirectToPage("/Error", new { errorMessage = roleResult.Errors.FirstOrDefault().Description });
            }

            var model = new NewEmployeeModel
            {
                AccountId = user.Id,
                Name = NewEmployeeInputModel.Name,
                Surname = NewEmployeeInputModel.Surname,
                Position = ApplicationCore.Enums.Position.Manager,
                Salary = NewEmployeeInputModel.Salary,
                EmploymentDate = NewEmployeeInputModel.EmploymentDate
            };

            var result = await _employeeService.AddEmployee(model);

            if (result.IsSuccess)
            {
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault() });
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            var accountId = await _employeeService.GetEmployeeAccountId(EditEmployeeInputModel.Id);

            var user = await _userManager.FindByIdAsync(accountId);
            if (user == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "Nie znaleziono u¿ytkownika." });
            }

            await _userStore.SetUserNameAsync(user, EditEmployeeInputModel.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, EditEmployeeInputModel.Email, CancellationToken.None);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault() });
            }

            var model = new EditEmployeeModel
            {
                Id = EditEmployeeInputModel.Id,
                Name = EditEmployeeInputModel.Name,
                Surname = EditEmployeeInputModel.Surname,
                Position = ApplicationCore.Enums.Position.Manager,
                Salary = EditEmployeeInputModel.Salary,
                EmploymentDate = EditEmployeeInputModel.EmploymentDate
            };

            var updateResult = await _employeeService.UpdateEmployee(model);

            if (updateResult.IsSuccess)
            {
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = updateResult.Errors.FirstOrDefault() });
            }
        }

        public async Task<IActionResult> OnPostDelete(int userId)
        {
            var accountId = await _employeeService.GetEmployeeAccountId(userId);
            var user = await _userManager.FindByIdAsync(accountId);

            if (user == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "Nie znaleziono u¿ytkownika." });
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault() });
            }

            var deleteResult = await _employeeService.DeleteEmployee(userId);

            if (deleteResult.IsSuccess)
            {
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault() });
            }
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}