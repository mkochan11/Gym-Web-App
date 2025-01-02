using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels.TrainingPlan.ClientDetails;

namespace Web.Pages.Klient.PlanyTreningowe
{
    [Authorize(Roles = "Client")]
    public class SzczegolyModel : PageModel
    {
        private readonly ITrainingPlanViewModelService _trainingPlanViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SzczegolyModel(
            ITrainingPlanViewModelService trainingPlanViewModelService,
            UserManager<ApplicationUser> userManager)
        {
            _trainingPlanViewModelService = trainingPlanViewModelService;
            _userManager = userManager;
        }

        public required ClientTrainingPlanDetailsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet(int planId)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _trainingPlanViewModelService.CheckIfClientCanSeeTrainingPlan(planId, user.Id);
            if (result.IsSuccess)
            {
                ViewModel = await _trainingPlanViewModelService.GetClientTrainingPlanDetailsViewModel(planId, user.Id);
                return Page();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault() });
            }
        }
    }
}
