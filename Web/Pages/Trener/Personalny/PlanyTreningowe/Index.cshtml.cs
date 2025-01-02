using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels.TrainingPlan.Index;

namespace Web.Pages.Trener.Personalny.PlanyTreningowe
{
    [Authorize(Roles = "PersonalTrainer")]
    public class IndexModel : PageModel
    {
        private readonly ITrainingPlanViewModelService _viewModelService;
        private readonly ITrainingPlanService _trainingPlanService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            ITrainingPlanViewModelService viewModelService,
            UserManager<ApplicationUser> userManager,
            ITrainingPlanService trainingPlanService)
        {
            _viewModelService = viewModelService;
            _userManager = userManager;
            _trainingPlanService = trainingPlanService;
        }

        public required TrainingPlansIndexViewModel ViewModel { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewModel = await _viewModelService.GetTrainingPlansIndexViewModel(user.Id);
        }

        public async Task<IActionResult> OnPostDelete(int planId)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _trainingPlanService.DeletePlan(planId, user.Id);

            if (result.IsSuccess)
            {
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault() });
            }
        }
    }
}
