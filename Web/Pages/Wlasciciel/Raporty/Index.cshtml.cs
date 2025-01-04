using ApplicationCore.Interfaces;
using ApplicationCore.Models.GymReport;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using Web.InputModels.Report;
using Web.Interfaces;
using Web.ViewModels.Owner.Reports;

namespace Web.Pages.Wlasciciel.Raporty
{
    [Authorize(Roles = "Owner")]
    public class IndexModel : PageModel
    {
        private readonly IOwnerReportsViewModelService _ownerReportsViewModelService;
        private readonly IGymReportService _gymReportService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IOwnerReportsViewModelService ownerReportsViewModelService, 
            UserManager<ApplicationUser> userManager,
            IGymReportService gymReportService)
        {
            _ownerReportsViewModelService = ownerReportsViewModelService;
            _userManager = userManager;
            _gymReportService = gymReportService;
        }

        public required OwnerReportsIndexViewModel ViewModel { get; set; }

        [BindProperty]
        public NewReportInputModel NewReportInput { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewModel = await _ownerReportsViewModelService.GetOwnerReportsIndexViewModel(user.Id);
        }

        public async Task<IActionResult> OnPostGenerate()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new NewGymReportModel
            {
                UserId = user.Id,
                Name = NewReportInput.Name,
                FromDate = NewReportInput.FromDate,
                ToDate = NewReportInput.ToDate,
                ClientsReport = NewReportInput.ClientsReport,
                BudgetReport = NewReportInput.BudgetReport,
                IndividualTrainingsReport = NewReportInput.IndividualTrainingsReport,
                GroupTrainingsReport = NewReportInput.GroupTrainingsReport
            };

            var result = await _gymReportService.GenerateReport(model);

            if (result.IsSuccess)
            {
                return RedirectToPage();
            }
            else
            {
                return RedirectToPage("/Error", new { errorMessage = result.Errors.FirstOrDefault()});
            }
        }

        public async Task<IActionResult> OnPostDelete(int reportId)
        {
            var result = await _gymReportService.DeleteRaport(reportId);

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
