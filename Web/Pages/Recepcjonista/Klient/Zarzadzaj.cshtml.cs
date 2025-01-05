using ApplicationCore.Interfaces;
using ApplicationCore.Models.Client;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;
using Web.InputModels.User;
using Web.Interfaces;
using Web.ViewModels.ManageClient;

namespace Web.Pages.Recepcjonista.Klient
{
    [Authorize(Roles = "Receptionist")]
    public class ZarzadzajModel : PageModel
    {
        private readonly IManageClientViewModelService _viewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClientService _clientService;

        public ZarzadzajModel(IManageClientViewModelService viewModelService,
            UserManager<ApplicationUser> userManager,
            IClientService clientService)
        {
            _viewModelService = viewModelService;
            _userManager = userManager;
            _clientService = clientService;
        }

        public string UserId { get; set; }

        public ManageClientIndexViewModel ViewModel { get; set; }

        [BindProperty]
        public EditUserInputModel EditUserInputModel { get; set; } = new EditUserInputModel();

        public string Message { get; set; }

        public async Task OnGet(string id)
        {
            UserId = id;
            ViewModel = await _viewModelService.GetManageClientIndexViewModel(UserId);
            EditUserInputModel.Name = ViewModel.Client.Name;
            EditUserInputModel.Surname = ViewModel.Client.Surname;
            EditUserInputModel.Email = ViewModel.Client.Email;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = await _clientService.GetClientAccountId(EditUserInputModel.Id);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("Nie znaleziono klienta");
            }

            user.Email = EditUserInputModel.Email;
            user.UserName = EditUserInputModel.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault().Description });
            }

            var model = new EditClientModel
            {
                Id = EditUserInputModel.Id,
                Name = EditUserInputModel.Name,
                Surname = EditUserInputModel.Surname,
            };

            var clientResult = await _clientService.UpdateClient(model);

            if (clientResult.IsSuccess)
            {
                Message = "Dane klienta zosta³y zaktualizowane.";
                return RedirectToPage(new {id = userId});
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = clientResult.Errors.FirstOrDefault() });
            }
        }
    }
}
