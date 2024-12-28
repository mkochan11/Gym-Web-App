using ApplicationCore.Interfaces;
using ApplicationCore.Models.Training;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.InputModels.Calendar.Trainer.Trainings.Group;
using Web.Interfaces;
using Web.ViewModels.Calendar.Trainer.Trainings.Group;

namespace Web.Pages.Trener.Grupowy.HarmonogramZajec
{
    [Authorize(Roles = "GroupTrainer")]
    public class IndexModel : PageModel
    {
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }

        private readonly ITrainingsCalendarViewModelService _trainingsCalendarViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGroupTrainingService _groupTrainingService;

        public IndexModel(
            ITrainingsCalendarViewModelService trainingsCalendarViewModelService,
            UserManager<ApplicationUser> userManager,
            IGroupTrainingService groupTrainingService)
        {
            _trainingsCalendarViewModelService = trainingsCalendarViewModelService;
            _userManager = userManager;
            _groupTrainingService = groupTrainingService;
        }

        public required GroupTrainingsCalendarIndexViewModel IndexViewModel { get; set; }

        [BindProperty]
        public NewGroupTrainingInputModel NewTrainingInputModel { get; set; }

        [BindProperty]
        public EditGroupTrainingInputModel EditTrainingInputModel { get; set; }

        public async Task OnGet()
        {
            CurrentMonth = HttpContext.Session.GetInt32("CurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("CurrentYear") ?? DateTime.Now.Year;

            if (TempData["ToastMessage"] != null)
            {
                var message = TempData["ToastMessage"].ToString();
            }

            var user = await _userManager.GetUserAsync(User);
            IndexViewModel = await _trainingsCalendarViewModelService.GetGroupTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear, user.Id);
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
            IndexViewModel = await _trainingsCalendarViewModelService.GetGroupTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear, user.Id);

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
            IndexViewModel = await _trainingsCalendarViewModelService.GetGroupTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear, user.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostCreateTraining()
        {
            if (TryValidateModel(NewTrainingInputModel))
            {
                NewGroupTrainingModel model = new NewGroupTrainingModel
                {
                    Date = NewTrainingInputModel.Date,
                    Hour = NewTrainingInputModel.Hour,
                    Duration = NewTrainingInputModel.Duration,
                    TrainingTypeId = NewTrainingInputModel.TrainingTypeId,
                    MaxParticipantNumber = NewTrainingInputModel.MaxParticipantNumber,
                    IsCyclic = NewTrainingInputModel.IsCyclic,
                    Description = NewTrainingInputModel.Description,
                    Repeatability = NewTrainingInputModel.IsCyclic is true ? NewTrainingInputModel.Repeatability : ""
                };

                var user = await _userManager.GetUserAsync(User);

                var result = await _groupTrainingService.CreateTraining(model, user.Id);

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
            else
            {
                TempData["ToastMessage"] = "Nie uda³o siê dodaæ treningu";
                TempData["ToastType"] = "warning";
                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnPostCancelTraining(int trainingId)
        {
            var user = await _userManager.GetUserAsync(User);

            var result = await _groupTrainingService.DeleteTraining(trainingId, user.Id);

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

        public async Task<IActionResult> OnPostEditTraining(int trainingId)
        {
            if (TryValidateModel(EditTrainingInputModel))
            {
                var user = await _userManager.GetUserAsync(User);

                var updatedTrainingModel = new EditGroupTrainingModel
                {
                    Id = trainingId,
                    Date = EditTrainingInputModel.Date,
                    Hour = EditTrainingInputModel.Hour,
                    Duration = EditTrainingInputModel.Duration,
                    Description = EditTrainingInputModel.Description,
                    TrainingTypeId = EditTrainingInputModel.TrainingTypeId,
                    MaxParticipantNumber = EditTrainingInputModel.MaxParticipantNumber                    
                };

                var result = await _groupTrainingService.UpdateTraining(updatedTrainingModel, user.Id);

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
    }
}
