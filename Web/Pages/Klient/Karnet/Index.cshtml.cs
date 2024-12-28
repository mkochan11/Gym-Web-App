using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Web.ViewModels.Membership;

namespace Web.Pages.Klient.Karnet
{
    [Authorize(Roles = "Client")]
    public class IndexModel : PageModel
    {
        private readonly IMembershipViewModelService _membershipViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(
            IMembershipViewModelService membershipViewModelService,
            UserManager<ApplicationUser> userManager
        )
        {
            _membershipViewModelService = membershipViewModelService;
            _userManager = userManager;
        }

        public required MembershipIndexViewModel MembershipViewModel { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            MembershipViewModel = await _membershipViewModelService.GetMembershipIndexViewModel(user.Id);
        }
    }
}
