using ApplicationCore.Entities;
using ApplicationCore.Entities.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels.Calendar.Trainings;

namespace Web.Pages.HarmonogramZajec
{
    public class IndexModel : PageModel
    {

        public int CurrentMonth { get; set; }
        public int CurrentYear { get; set; }
        private readonly ITrainingsCalendarViewModelService _trainingsCalendarViewModelService;

        public IndexModel(
            ITrainingsCalendarViewModelService trainingsCalendarViewModelService
            )
        {
            _trainingsCalendarViewModelService = trainingsCalendarViewModelService;
        }

        public required TrainingsCalendarIndexViewModel ViewModel { get; set; }

        public async Task OnGet()
        {
            CurrentMonth = DateTime.Now.Month;
            CurrentYear = DateTime.Now.Year;

            ViewModel = await _trainingsCalendarViewModelService.GetTrainingsCalendarIndexViewModel(CurrentMonth, CurrentYear);
        }

        public async Task<IActionResult> OnPostNext()
        {
            return Page();
        }
    }
}
