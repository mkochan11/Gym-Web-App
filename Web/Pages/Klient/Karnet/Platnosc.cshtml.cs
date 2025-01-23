using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Membership;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Klient.Karnet
{
    public class PlatnoscModel : PageModel
    {
        private readonly IMembershipService _membershipService;
        private readonly UserManager<ApplicationUser> _userManager;
        public required List<string> PaymentMethods { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public string SelectedPaymentMethod { get; set; }

        public PlatnoscModel(
            IMembershipService membershipService,
            UserManager<ApplicationUser> userManager
            )
        {
            _membershipService = membershipService;
            _userManager = userManager;
            PaymentMethods = new List<string>();
        }
        public void OnGet()
        {
            PaymentMethods = Enum.GetValues(typeof(PaymentMethod))
                .Cast<PaymentMethod>()
                .Select(p => p.ToString())
                .ToList();
        }

        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(SelectedPaymentMethod))
            {
                ModelState.AddModelError("", "Please select a payment method.");
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            var newMembershipModel = new NewMembershipModel
            {
                MembershipPlanId = Id,
                UserId = user.Id,
                PaymentMethod = SelectedPaymentMethod,
            };

            var response = await _membershipService.AddGymMembership(newMembershipModel);

            if (response.IsSuccess)
            {
                return RedirectToPage("./PlatnoscPowodzenie");
            }
            else
            {
                return RedirectToPage("/Error", new {errorMessage = response.Errors.FirstOrDefault()});
            }
        }
    }
}