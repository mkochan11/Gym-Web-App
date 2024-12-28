using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IndexModel(
            ITrainingsCalendarViewModelService trainingsCalendarViewModelService,
            UserManager<ApplicationUser> userManager)
        {
            _trainingsCalendarViewModelService = trainingsCalendarViewModelService;
            _userManager = userManager;
        }

        public required GroupTrainingsCalendarIndexViewModel IndexViewModel { get; set; }

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
    }
}
