using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.ZarzadzajUzytkownikami
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public JsonResult OnGetGetUsers(string role)
        {
            // Example: Replace this with your database fetching logic.
            var users = role switch
            {
                "Owners" => new List<object>
            {
                new { name = "Owner 1", email = "owner1@example.com" },
                new { name = "Owner 2", email = "owner2@example.com" }
            },
                "Managers" => new List<object>
            {
                new { name = "Manager 1", email = "manager1@example.com" },
                new { name = "Manager 2", email = "manager2@example.com" }
            },
                "Trainers" => new List<object>
            {
                new { name = "Trainer 1", email = "trainer1@example.com" },
                new { name = "Trainer 2", email = "trainer2@example.com" }
            },
                "Receptionists" => new List<object>
            {
                new { name = "Receptionist 1", email = "receptionist1@example.com" },
                new { name = "Receptionist 2", email = "receptionist2@example.com" }
            },
                "Clients" => new List<object>
            {
                new { name = "Client 1", email = "client1@example.com" },
                new { name = "Client 2", email = "client2@example.com" }
            },
                _ => new List<object>()
            };

            return new JsonResult(users);
        }
    }
}
