using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels.Calendar.Client.TrainingsHistory;

namespace Web.Pages.Klient.HistoriaTreningów
{
    [Authorize(Roles = "Client")]
    public class IndexModel : PageModel
    {
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }

        private readonly ITrainingsHistoryViewModelService _trainingsHistoryViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            ITrainingsHistoryViewModelService trainingsHistoryViewModelService,
            UserManager<ApplicationUser> userManager
            )
        {
            _trainingsHistoryViewModelService = trainingsHistoryViewModelService;
            _userManager = userManager;
        }

        public required ClientTrainingsHistoryIndexViewModel ViewModel {  get; set; }

        public async Task OnGet()
        {
            CurrentMonth = HttpContext.Session.GetInt32("CurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("CurrentYear") ?? DateTime.Now.Year;

            var user = await _userManager.GetUserAsync(User);
            ViewModel = await _trainingsHistoryViewModelService.GetClientTrainingsHistoryIndexViewModel(CurrentMonth, CurrentYear, user.Id);
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
            ViewModel = await _trainingsHistoryViewModelService.GetClientTrainingsHistoryIndexViewModel(CurrentMonth, CurrentYear, user.Id);

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
            ViewModel = await _trainingsHistoryViewModelService.GetClientTrainingsHistoryIndexViewModel(CurrentMonth, CurrentYear, user.Id);

            return Page();
        }
    }
}
