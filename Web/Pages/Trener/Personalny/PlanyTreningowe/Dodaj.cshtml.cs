using ApplicationCore.Models.Exercise;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.InputModels.TrainingPlan;
using Web.Interfaces;
using Web.ViewModels.TrainingPlan.Add;

namespace Web.Pages.Trener.Personalny.PlanyTreningowe
{
    [Authorize(Roles = "PersonalTrainer")]
    public class DodajModel : PageModel
    {
        private readonly ITrainingPlanViewModelService _trainingPlanViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DodajModel(
            UserManager<ApplicationUser> userManager,
            ITrainingPlanViewModelService trainingPlanViewModelService)
        {
            _userManager = userManager;
            _trainingPlanViewModelService = trainingPlanViewModelService;
        }

        public required AddTrainingPlanIndexViewModel ViewModel {  get; set; }

        [BindProperty]
        public NewExerciseInputModel NewExerciseInputModel { get; set; }

        [BindProperty]
        public NewTrainingPlanInputModel NewTrainingPlanInputModel { get; set; }

        public List<NewExerciseModel> Exercises { get; set; } = new List<NewExerciseModel>();
        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewModel = await _trainingPlanViewModelService.GetAddTrainingPlanIndexViewModel(user.Id);

            var exercisesJson = HttpContext.Session.GetString("Exercises");
            Exercises = !string.IsNullOrEmpty(exercisesJson)
                ? JsonConvert.DeserializeObject<List<NewExerciseModel>>(exercisesJson)
                : new List<NewExerciseModel>();
        }

        public async Task<IActionResult> OnPostAddExercise()
        {
            int nextId = HttpContext.Session.GetInt32("nextId") ?? 1;

            var exercisesJson = HttpContext.Session.GetString("Exercises");
            var exercises = !string.IsNullOrEmpty(exercisesJson)
                ? JsonConvert.DeserializeObject<List<NewExerciseModel>>(exercisesJson)
                : new List<NewExerciseModel>();

            var newExercise = new NewExerciseModel
            {
                Id = nextId,
                Name = NewExerciseInputModel.Name,
                Description = NewExerciseInputModel.Description,
                RepetitionsNumber = NewExerciseInputModel.RepetitionsNumber,
                SeriesNumber = NewExerciseInputModel.SeriesNumber,
                RestTime = NewExerciseInputModel.RestTime,
            };
            exercises.Add(newExercise);

            HttpContext.Session.SetString("Exercises", JsonConvert.SerializeObject(exercises));
            nextId++;
            HttpContext.Session.SetInt32("nextId", nextId);

            return RedirectToPage();
        }

        public IActionResult OnPostDeleteExercise(int exerciseId)
        {
            var exercisesJson = HttpContext.Session.GetString("Exercises");
            var exercises = !string.IsNullOrEmpty(exercisesJson)
                ? JsonConvert.DeserializeObject<List<NewExerciseModel>>(exercisesJson)
                : new List<NewExerciseModel>();

            var exerciseToRemove = exercises.FirstOrDefault(e => e.Id == exerciseId);
            if (exerciseToRemove != null)
            {
                exercises.Remove(exerciseToRemove);
            }

            HttpContext.Session.SetString("Exercises", JsonConvert.SerializeObject(exercises));

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSaveTrainingPlan()
        {
                return RedirectToPage("./Index");

            //TODO: Flush session exercises, nextId when saving training plan
        }
    }
}
