using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels.Membership.Buy;

namespace Web.Pages.Klient.Karnet
{
    public class KupModel : PageModel
    {

        public required MembershipBuyViewModel ViewModel { get; set; }
        private readonly IMembershipBuyViewModelService _membershipBuyViewModelService;

        public KupModel(IMembershipBuyViewModelService membershipBuyViewModelService)
        {
            _membershipBuyViewModelService = membershipBuyViewModelService;
        }
        public async Task OnGet()
        {
            ViewModel = await _membershipBuyViewModelService.GetMembershipBuyViewModel();
        }
    }
}
