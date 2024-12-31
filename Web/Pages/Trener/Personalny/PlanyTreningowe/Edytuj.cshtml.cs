using ApplicationCore.Interfaces;
using ApplicationCore.Models.Exercise;
using ApplicationCore.Models.TrainingPlan;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.InputModels.TrainingPlan;
using Web.Interfaces;
using Web.ViewModels.TrainingPlan.Edit;

namespace Web.Pages.Trener.Personalny.PlanyTreningowe
{
    [Authorize(Roles = "PersonalTrainer")]
    public class EdytujModel : PageModel
    {
        private readonly ITrainingPlanViewModelService _trainingPlanViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITrainingPlanService _trainingPlanService;

        public EdytujModel(
            UserManager<ApplicationUser> userManager,
            ITrainingPlanViewModelService trainingPlanViewModelService,
            ITrainingPlanService trainingPlanService)
        {
            _userManager = userManager;
            _trainingPlanViewModelService = trainingPlanViewModelService;
            _trainingPlanService = trainingPlanService;
        }

        public required EditTrainingPlanIndexViewModel ViewModel {  get; set; }

        [BindProperty]
        public NewExerciseInputModel NewExerciseInputModel { get; set; }

        [BindProperty]
        public EditExerciseInputModel EditExerciseInputModel { get; set; }

        [BindProperty]
        public NewTrainingPlanInputModel EditTrainingPlanInputModel { get; set; }
        
        public async Task OnGet(int id)
        {
            if (!HttpContext.Session.GetInt32("planId").HasValue) { HttpContext.Session.SetInt32("planId", id); };

            var user = await _userManager.GetUserAsync(User);
            ViewModel = await _trainingPlanViewModelService.GetEditTrainingPlanIndexViewModel(id, user.Id);

            EditTrainingPlanInputModel = new NewTrainingPlanInputModel
            {
                Name = ViewModel.TrainingPlanItem.Name,
                Description = ViewModel.TrainingPlanItem.Description,
                ClientId = ViewModel.TrainingPlanItem.ClientId,
            };

        }

        public async Task<IActionResult> OnPostSaveTrainingPlanChanges(int id)
        {
            var trainingPlanModel = new EditTrainingPlanModel
            {
                Id = id,
                Name = EditTrainingPlanInputModel.Name,
                Description = EditTrainingPlanInputModel.Description,
                ClientId = EditTrainingPlanInputModel.ClientId,
            };
            var user = await _userManager.GetUserAsync(User);
            var result = await _trainingPlanService.UpdateTrainingPlan(trainingPlanModel, user.Id);

            if (result.IsSuccess)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.First() });
            }
        }

        public async Task<IActionResult> OnPostAddExercise()
        {

            var newExercise = new NewExerciseModel
            {
                Id = HttpContext.Session.GetInt32("planId").Value,
                Name = NewExerciseInputModel.Name,
                Description = NewExerciseInputModel.Description,
                RepetitionsNumber = NewExerciseInputModel.RepetitionsNumber,
                SeriesNumber = NewExerciseInputModel.SeriesNumber,
                RestTime = NewExerciseInputModel.RestTime,
            };

            var user = await _userManager.GetUserAsync(User);
            var result = await _trainingPlanService.AddNewExercise(newExercise, user.Id);

            if (result.IsSuccess)
            {
                return RedirectToPage(new { id = newExercise.Id });
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.First() });
            }
        }

        public async Task<IActionResult> OnPostEditExercise(int exerciseId)
        {
            var exerciseModel = new EditExerciseModel
            {
                Id = exerciseId,
                Name = EditExerciseInputModel.Name,
                Description = EditExerciseInputModel.Description,
                SeriesNumber = EditExerciseInputModel.SeriesNumber,
                RepetitionsNumber = EditExerciseInputModel.RepetitionsNumber,
                RestTime = EditExerciseInputModel.RestTime,
            };

            var user = await _userManager.GetUserAsync(User);
            var result = await _trainingPlanService.EditExercise(exerciseModel, user.Id);

            if (result.IsSuccess)
            {
                return RedirectToPage(new { id = HttpContext.Session.GetInt32("planId").Value });
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.First() });
            }
        }

        public async Task<IActionResult> OnPostDeleteExercise(int exerciseId)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _trainingPlanService.DeleteExercise(exerciseId, user.Id);

            if (result.IsSuccess)
            {
                return RedirectToPage(new { id = HttpContext.Session.GetInt32("planId").Value });
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.First() });
            }
        }
    }
}
