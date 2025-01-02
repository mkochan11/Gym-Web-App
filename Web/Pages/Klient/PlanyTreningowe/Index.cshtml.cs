using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels.TrainingPlan.Index;

namespace Web.Pages.Klient.PlanyTreningowe
{
    [Authorize(Roles = "Client")]
    public class IndexModel : PageModel
    {
        private readonly ITrainingPlanViewModelService _viewModelService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            ITrainingPlanViewModelService viewModelService,
            UserManager<ApplicationUser> userManager)
        {
            _viewModelService = viewModelService;
            _userManager = userManager;
        }

        public required ClientTrainingPlansIndexViewModel ViewModel { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewModel = await _viewModelService.GetClientTrainingPlanIndexViewModel(user.Id);
        }
    }
}
