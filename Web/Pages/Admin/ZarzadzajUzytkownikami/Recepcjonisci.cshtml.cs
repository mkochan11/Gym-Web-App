using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Employee;
using ApplicationCore.Models.User;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.InputModels.User;
using Web.Interfaces;
using Web.ViewModels.Admin.ManageUsers;

namespace Web.Pages.Admin.ZarzadzajUzytkownikami
{
    [Authorize(Roles = "Administrator")]
    public class RecepcjonisciModel : PageModel
    {
        private readonly IManageUsersViewModelService<Receptionist> _manageUsersViewModelService;
        private readonly IEmployeeService<Receptionist> _employeeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public RecepcjonisciModel(IManageUsersViewModelService<Receptionist> manageUsersViewModelService,
            IEmployeeService<Receptionist> employeeService,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore)
        {
            _manageUsersViewModelService = manageUsersViewModelService;
            _employeeService = employeeService;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }
        public required ManageUsersIndexViewModel ViewModel { get; set; }

        [BindProperty]
        public NewUserInputModel NewEmployeeInputModel { get; set; }

        [BindProperty]
        public EditUserInputModel EditEmployeeInputModel { get; set; }

        public async Task OnGet()
        {
            ViewModel = await _manageUsersViewModelService.GetManageUsersIndexViewModel();
        }

        public async Task<IActionResult> OnPostCreate()
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, NewEmployeeInputModel.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, NewEmployeeInputModel.Email, CancellationToken.None);
            var password = GenerateRandomPassword();
            var registerResult = await _userManager.CreateAsync(user, password);

            if (!registerResult.Succeeded)
            {
                return RedirectToPage("/Error", new { errorMessage = registerResult.Errors.FirstOrDefault() });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Receptionist");
            if (!roleResult.Succeeded)
            {
                return RedirectToPage("/Error", new { errorMessage = roleResult.Errors.FirstOrDefault() });
            }

            var model = new NewEmployeeModel
            {
                AccountId = user.Id,
                Name = NewEmployeeInputModel.Name,
                Surname = NewEmployeeInputModel.Surname,
                Position = ApplicationCore.Enums.Position.Receptionist
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
                Position = ApplicationCore.Enums.Position.Receptionist
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

        private string GenerateRandomPassword()
        {
            const int length = 12;
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_-+=<>?";

            var random = new Random();
            var passwordChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                passwordChars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(passwordChars);
        }
    }
}
