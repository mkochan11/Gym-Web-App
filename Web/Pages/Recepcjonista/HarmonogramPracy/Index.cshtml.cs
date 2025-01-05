using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;
using Web.Interfaces;
using Web.ViewModels.WorkSchedule.Receptionist;

namespace Web.Pages.Recepcjonista.HarmonogramPracy
{
    [Authorize(Roles = "Receptionist")]
    public class IndexModel : PageModel
    {
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }

        private readonly IWorkScheduleViewModelService _viewModelService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            IWorkScheduleViewModelService viewModelService
            )
        {
            _userManager = userManager;
            _viewModelService = viewModelService;
        }

        public required ReceptionistWorkScheduleIndexViewModel IndexViewModel { get; set; }

        public async Task OnGet()
        {
            CurrentMonth = HttpContext.Session.GetInt32("RWSCurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("RWSCurrentYear") ?? DateTime.Now.Year;

            var user = await _userManager.GetUserAsync(User);
            IndexViewModel = await _viewModelService.GetReceptionistWorkScheduleIndexViewModel(CurrentMonth, CurrentYear, user.Id);
        }

        public async Task<IActionResult> OnPostNext()
        {
            CurrentMonth = HttpContext.Session.GetInt32("RWSCurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("RWSCurrentYear") ?? DateTime.Now.Year;

            if (CurrentMonth == 12)
            {
                CurrentMonth = 1;
                CurrentYear += 1;
            }
            else
            {
                CurrentMonth++;
            }

            HttpContext.Session.SetInt32("RWSCurrentMonth", CurrentMonth);
            HttpContext.Session.SetInt32("RWSCurrentYear", CurrentYear);

            var user = await _userManager.GetUserAsync(User);
            IndexViewModel = await _viewModelService.GetReceptionistWorkScheduleIndexViewModel(CurrentMonth, CurrentYear, user.Id);

            return Page();
        }

        public async Task<IActionResult> OnPostPrevious()
        {
            CurrentMonth = HttpContext.Session.GetInt32("RWSCurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("RWSCurrentYear") ?? DateTime.Now.Year;

            if (CurrentMonth == 1)
            {
                CurrentMonth = 12;
                CurrentYear--;
            }
            else
            {
                CurrentMonth--;
            }

            HttpContext.Session.SetInt32("RWSCurrentMonth", CurrentMonth);
            HttpContext.Session.SetInt32("RWSCurrentYear", CurrentYear);

            var user = await _userManager.GetUserAsync(User);
            IndexViewModel = await _viewModelService.GetReceptionistWorkScheduleIndexViewModel(CurrentMonth, CurrentYear, user.Id);

            return Page();
        }
    }
}
