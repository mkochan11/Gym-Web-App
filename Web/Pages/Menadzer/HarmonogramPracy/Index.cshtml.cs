using ApplicationCore.Interfaces;
using ApplicationCore.Models.Shift;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;
using Web.InputModels.Shift;
using Web.Interfaces;
using Web.Services;
using Web.ViewModels.WorkSchedule.Manager;

namespace Web.Pages.Menadzer.HarmonogramPracy
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }

        private readonly IWorkScheduleViewModelService _viewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IShiftService _shiftService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            IWorkScheduleViewModelService viewModelService,
            IShiftService shiftService
            )
        {
            _userManager = userManager;
            _viewModelService = viewModelService;
            _shiftService = shiftService;
        }

        public required ManagerWorkScheduleIndexViewModel IndexViewModel { get; set; }

        [BindProperty]
        public NewShiftInputModel NewShiftInputModel { get; set; }

        [BindProperty]
        public EditShiftInputModel EditShiftInputModel { get; set; }

        public async Task OnGet()
        {
            CurrentMonth = HttpContext.Session.GetInt32("MWSCurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("MWSCurrentYear") ?? DateTime.Now.Year;

            IndexViewModel = await _viewModelService.GetManagerWorkScheduleIndexViewModel(CurrentMonth, CurrentYear);
        }

        public async Task<IActionResult> OnPostNext()
        {
            CurrentMonth = HttpContext.Session.GetInt32("MWSCurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("MWSCurrentYear") ?? DateTime.Now.Year;

            if (CurrentMonth == 12)
            {
                CurrentMonth = 1;
                CurrentYear += 1;
            }
            else
            {
                CurrentMonth++;
            }

            HttpContext.Session.SetInt32("MWSCurrentMonth", CurrentMonth);
            HttpContext.Session.SetInt32("MWSCurrentYear", CurrentYear);

            IndexViewModel = await _viewModelService.GetManagerWorkScheduleIndexViewModel(CurrentMonth, CurrentYear);

            return Page();
        }

        public async Task<IActionResult> OnPostPrevious()
        {
            CurrentMonth = HttpContext.Session.GetInt32("MWSCurrentMonth") ?? DateTime.Now.Month;
            CurrentYear = HttpContext.Session.GetInt32("MWSCurrentYear") ?? DateTime.Now.Year;

            if (CurrentMonth == 1)
            {
                CurrentMonth = 12;
                CurrentYear--;
            }
            else
            {
                CurrentMonth--;
            }

            HttpContext.Session.SetInt32("MWSCurrentMonth", CurrentMonth);
            HttpContext.Session.SetInt32("MWSCurrentYear", CurrentYear);

            IndexViewModel = await _viewModelService.GetManagerWorkScheduleIndexViewModel(CurrentMonth, CurrentYear);

            return Page();
        }

        public async Task<IActionResult> OnPostCreateShift()
        {
            var newShift = new NewShiftModel
            {
                Date = NewShiftInputModel.Date,
                StartHour = NewShiftInputModel.StartHour,
                EndHour = NewShiftInputModel.EndHour,
                ReceptionistId = NewShiftInputModel.ReceptionistId
            };

            var result = await _shiftService.CreateShift(newShift);

            if (result.IsSuccess)
            {
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault() });
            }
        }

        public async Task<IActionResult> OnPostEditShift(int shiftId)
        {
            var shift = new EditShiftModel
            {
                ShiftId = shiftId,
                StartHour = NewShiftInputModel.StartHour,
                EndHour = NewShiftInputModel.EndHour,
                ReceptionistId = NewShiftInputModel.ReceptionistId
            };

            var result = await _shiftService.UpdateShift(shift);

            if (result.IsSuccess)
            {
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault() });
            }
        }

        public async Task<IActionResult> OnPostCancelShift(int shiftId)
        {
            var result = await _shiftService.DeleteShift(shiftId);

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
