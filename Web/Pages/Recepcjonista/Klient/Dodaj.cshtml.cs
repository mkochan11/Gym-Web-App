using ApplicationCore.Constants;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Client;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;
using Web.InputModels.User;

namespace Web.Pages.Recepcjonista.Klient
{
    [Authorize(Roles = "Receptionist")]
    public class DodajModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IClientService _clientService;

        public DodajModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            IClientService clientService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _clientService = clientService;
        }

        [BindProperty]
        public NewUserInputModel Input { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, AuthorizationConstants.DEFAULT_CLIENT_PASSWORD);

            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Client");
                if (!roleResult.Succeeded)
                {
                    return RedirectToPage("Error");
                }

                var userId = await _userManager.GetUserIdAsync(user);

                var clientModel = new NewClientModel
                {
                    Name = Input.Name,
                    Surname = Input.Surname,
                    AccountId = userId
                };

                var client_result = await _clientService.AddClient(clientModel);

                if (!client_result.IsSuccess)
                {
                    return RedirectToPage("Error");
                }

                return RedirectToPage("Zarzadzaj", new { id = user.Id });
            }
            else
            {
                return RedirectToPage("Error");
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
