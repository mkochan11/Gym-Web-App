using ApplicationCore.Interfaces;
using ApplicationCore.Models.EmployeeReport;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web.Mvc;
using Web.InputModels.Employee;
using Web.InputModels.Report;
using Web.Interfaces;
using Web.ViewModels.Manager.Reports;

namespace Web.Pages.Menadzer.Raporty
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        private readonly IReportsViewModelService _managerReportsViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeReportService _employeeReportService;

        public IndexModel(IReportsViewModelService managerReportsViewModelService,
            UserManager<ApplicationUser> userManager,
            IEmployeeReportService employeeReportService)
        {
            _managerReportsViewModelService = managerReportsViewModelService;
            _userManager = userManager;
            _employeeReportService = employeeReportService;
        }

        public required ManagerReportsIndexViewModel ViewModel { get; set; }

        [BindProperty]
        public NewEmployeeReportInputModel NewReportInput { get; set; }

        public async Task OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewModel = await _managerReportsViewModelService.GetManagerReportsIndexViewModel(user.Id);
        }

        public async Task<IActionResult> OnPostCreate()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new NewEmployeeReportModel
            {
                EmployeeId = NewReportInput.EmployeeId,
                ManagerId = user.Id,
                Name = NewReportInput.Name,
                FromDate = NewReportInput.FromDate,
                ToDate = NewReportInput.ToDate,
                Position = NewReportInput.Position
            };

            var result = await _employeeReportService.GenerateReport(model);

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
            var result = await _employeeReportService.DeleteReport(reportId);

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
