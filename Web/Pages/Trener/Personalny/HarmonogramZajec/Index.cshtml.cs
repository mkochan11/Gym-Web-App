using ApplicationCore.Interfaces;
using ApplicationCore.Models.Training;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.InputModels.Calendar.Trainer.Trainings.Personal;
using Web.Interfaces;
using Web.ViewModels.Calendar.Trainer.Trainings.Personal;

namespace Web.Pages.Trener.Personalny.HarmonogramZajec
{
    [Authorize(Roles = "PersonalTrainer")]
    public class IndexModel : PageModel
    {
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }

        private readonly ITrainingsCalendarViewModelService _trainingsCalendarViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIndividualTrainingService _individualTrainingService;

        public IndexModel(
            ITrainingsCalendarViewModelService trainingsCalendarViewModelService,
            UserManager<ApplicationUser> userManager,
            IIndividualTrainingService individualTrainingService
            )
        {
            _trainingsCalendarViewModelService = trainingsCalendarViewModelService;
            _userManager = userManager;
            _individualTrainingService = individualTrainingService;
        }

        public required PersonalTrainingsCalendarIndexViewModel IndexViewModel { get; set; }

        [BindProperty]
        public NewIndividualTrainingInputModel NewTrainingInputModel { get; set; }

        [BindProperty]
        public EditIndividualTrainingInputModel EditTrainingInputModel { get; set; }

        public async Task OnGet()
        {
            CurrentMonth = HttpContext.Session.GetInt32("CurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("CurrentYear") ?? DateTime.Now.Year;

            if (TempData["ToastMessage"] != null)
            {
                var message = TempData["ToastMessage"].ToString();
            }


            var user = await _userManager.GetUserAsync(User);
            IndexViewModel = await _trainingsCalendarViewModelService.GetPersonalTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear, user.Id);
        }

        public async Task<IActionResult> OnPostNext()
        {
            CurrentMonth = HttpContext.Session.GetInt32("CurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("CurrentYear") ?? DateTime.Now.Year;

            if (CurrentMonth == 12)
            {
                CurrentMonth = 1;
                CurrentYear += 1;
            }
            else
            {
                CurrentMonth++;
            }

            HttpContext.Session.SetInt32("CurrentMonth", CurrentMonth);
            HttpContext.Session.SetInt32("CurrentYear", CurrentYear);

            var user = await _userManager.GetUserAsync(User);
            IndexViewModel = await _trainingsCalendarViewModelService.GetPersonalTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear, user.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostPrevious()
        {
            CurrentMonth = HttpContext.Session.GetInt32("CurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("CurrentYear") ?? DateTime.Now.Year;

            if (CurrentMonth == 1)
            {
                CurrentMonth = 12;
                CurrentYear--;
            }
            else
            {
                CurrentMonth--;
            }

            HttpContext.Session.SetInt32("CurrentMonth", CurrentMonth);
            HttpContext.Session.SetInt32("CurrentYear", CurrentYear);

            var user = await _userManager.GetUserAsync(User);
            IndexViewModel = await _trainingsCalendarViewModelService.GetPersonalTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear, user.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostEditTraining(int trainingId)
        {
            if (TryValidateModel(EditTrainingInputModel))
            {
                var user = await _userManager.GetUserAsync(User);

                var updatedTrainingModel = new EditIndividualTrainingModel
                {
                    Id = trainingId,
                    Date = EditTrainingInputModel.Date,
                    Hour = EditTrainingInputModel.Hour,
                    Duration = EditTrainingInputModel.Duration,
                    Description = EditTrainingInputModel.Description
                };

                var result = await _individualTrainingService.UpdateTraining(updatedTrainingModel, user.Id);

                if (result.IsSuccess)
                {
                    TempData["ToastMessage"] = "Pomyœlnie zedytowano trening";
                    TempData["ToastType"] = "success";
                    return RedirectToPage();
                }
                else
                {
                    return RedirectToPage("/Error", new { errorMessage = result.Errors.First() });
                }
            }
            else
            {
                TempData["ToastMessage"] = "Nie uda³o siê edytowaæ treningu";
                TempData["ToastType"] = "warning";

                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnPostCancelTraining(int trainingId)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _individualTrainingService.DeleteTraining(trainingId, user.Id);

            if (result.IsSuccess)
            {
                TempData["ToastMessage"] = "Pomyœlnie odwo³ano trening";
                TempData["ToastType"] = "success";
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.First() });
            }
        }

        public async Task<IActionResult> OnPostCreateTraining()
        {
            var user = await _userManager.GetUserAsync(User);

            NewIndividualTrainingModel model = new NewIndividualTrainingModel
            {
                Date = NewTrainingInputModel.Date,
                Hour = NewTrainingInputModel.Hour,
                Duration = NewTrainingInputModel.Duration,
                IsCyclic = NewTrainingInputModel.IsCyclic,
                Description = NewTrainingInputModel.Description,
                Repeatability = NewTrainingInputModel.IsCyclic is true ? NewTrainingInputModel.Repeatability : ""
            };

            var result = await _individualTrainingService.CreateTraining(model, user.Id);

            if (result.IsSuccess)
            {
                TempData["ToastMessage"] = "Pomyœlnie dodano trening";
                TempData["ToastType"] = "success";
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.First() });
            }
        }
    }
}
