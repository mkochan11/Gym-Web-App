using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;

namespace Web.Pages.Recepcjonista.Klient
{
    [Authorize(Roles = "Receptionist")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string ClientLogin { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(ClientLogin))
            {
                Message = "Wprowadü poprawny login Klienta.";
                return Page();
            }

            var user = await _userManager.FindByNameAsync(ClientLogin);
            if (user == null)
            {
                Message = "Nie znaleziono Klienta o podanym loginie";
                return Page();
            }

            return RedirectToPage("Zarzadzaj", new { id = user.Id });
        }
    }
}
