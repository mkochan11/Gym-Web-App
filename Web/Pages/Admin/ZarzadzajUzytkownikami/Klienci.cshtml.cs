using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Client;
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
    public class KlienciModel : PageModel
    {
        private readonly IManageUsersViewModelService<Client> _manageUsersViewModelService;
        private readonly IClientService _clientService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;

        public KlienciModel(IManageUsersViewModelService<Client> manageUsersViewModelService,
            IClientService clientService,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore)
        {
            _manageUsersViewModelService = manageUsersViewModelService;
            _clientService = clientService;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }

        public required ManageUsersIndexViewModel ViewModel { get; set; }

        [BindProperty]
        public NewUserInputModel NewClientInputModel { get; set; }

        [BindProperty]
        public EditUserInputModel EditClientInputModel { get; set; }

        public async Task OnGet()
        {
            ViewModel = await _manageUsersViewModelService.GetManageUsersIndexViewModel();
        }

        public async Task<IActionResult> OnPostCreate()
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, NewClientInputModel.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, NewClientInputModel.Email, CancellationToken.None);
            var password = GenerateRandomPassword();
            var registerResult = await _userManager.CreateAsync(user, password);

            if (!registerResult.Succeeded)
            {
                return RedirectToPage("/Error", new { errorMessage = registerResult.Errors.FirstOrDefault() });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Client");
            if (!roleResult.Succeeded)
            {
                return RedirectToPage("/Error", new { errorMessage = roleResult.Errors.FirstOrDefault() });
            }

            var model = new NewClientModel
            {
                AccountId = user.Id,
                Name = NewClientInputModel.Name,
                Surname = NewClientInputModel.Surname
            };

            var result = await _clientService.AddClient(model);

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
            var accountId = await _clientService.GetClientAccountId(EditClientInputModel.Id);

            var user = await _userManager.FindByIdAsync(accountId);
            if (user == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "Nie znaleziono u¿ytkownika." });
            }

            await _userStore.SetUserNameAsync(user, EditClientInputModel.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, EditClientInputModel.Email, CancellationToken.None);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault() });
            }

            var model = new EditClientModel
            {
                Id = EditClientInputModel.Id,
                Name = EditClientInputModel.Name,
                Surname = EditClientInputModel.Surname,
            };

            var updateResult = await _clientService.UpdateClient(model);

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
            var accountId = await _clientService.GetClientAccountId(userId);
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

            var deleteResult = await _clientService.DeleteClient(userId);

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
