using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Wlasciciel.Raporty
{
    [Authorize(Roles = "Owner")]
    public class IndexModel : PageModel
    {

        public void OnGet()
        {
        }
    }
}
