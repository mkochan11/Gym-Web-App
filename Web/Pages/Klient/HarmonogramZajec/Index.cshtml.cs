using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels.Calendar.Client.Trainings;

namespace Web.Pages.Klient.HarmonogramZajec
{
    [Authorize(Roles = "Client")]
    public class IndexModel : PageModel
    {
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }

        private readonly ITrainingsCalendarViewModelService _trainingsCalendarViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGroupTrainingService _groupTrainingService;
        private readonly IIndividualTrainingService _individualTrainingService;

        public IndexModel(
            ITrainingsCalendarViewModelService trainingsCalendarViewModelService,
            UserManager<ApplicationUser> userManager,
            IGroupTrainingService groupTrainingService,
            IIndividualTrainingService individualTrainingService
            )
        {
            _trainingsCalendarViewModelService = trainingsCalendarViewModelService;
            _userManager = userManager;
            _groupTrainingService = groupTrainingService;
            _individualTrainingService = individualTrainingService;
        }

        public required ClientTrainingsCalendarIndexViewModel ViewModel { get; set; }

        public async Task OnGet()
        {
            CurrentMonth = HttpContext.Session.GetInt32("CurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("CurrentYear") ?? DateTime.Now.Year;

            if (TempData["ToastMessage"] != null)
            {
                var message = TempData["ToastMessage"].ToString();
            }

            var user = await _userManager.GetUserAsync(User);
            ViewModel = await _trainingsCalendarViewModelService.GetClientTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear, user.Id);
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
            ViewModel = await _trainingsCalendarViewModelService.GetClientTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear, user.Id);

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
            ViewModel = await _trainingsCalendarViewModelService.GetClientTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear, user.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostReserveGroup(int trainingId)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _groupTrainingService.ReservePlace(trainingId, user.Id);
            if (result.IsSuccess)
            {
                TempData["ToastMessage"] = "Pomyœlnie zarezerwowano miejsce na treningu";
                TempData["ToastType"] = "success";
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.First()});
            }
        }

        public async Task<IActionResult> OnPostCancelGroup(int trainingId)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _groupTrainingService.CancelPlace(trainingId, user.Id);
            if (result.IsSuccess)
            {
                TempData["ToastMessage"] = "Pomyœlnie odwo³ano rezerwacjê miejsca na treningu";
                TempData["ToastType"] = "success";
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.First() });
            }
        }

        public async Task<IActionResult> OnPostReserveIndividual(int trainingId)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _individualTrainingService.Reserve(trainingId, user.Id);
            if (result.IsSuccess)
            {
                TempData["ToastMessage"] = "Pomyœlnie zarezerwowano trening";
                TempData["ToastType"] = "success";
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.First() });
            }
        }

        public async Task<IActionResult> OnPostCancelIndividual(int trainingId)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _individualTrainingService.CancelReservation(trainingId, user.Id);
            if (result.IsSuccess)
            {
                TempData["ToastMessage"] = "Pomyœlnie odwo³ano rezerwacjê treningu";
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
