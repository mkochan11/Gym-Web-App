
using ApplicationCore.Models.EmployeeReport;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IEmployeeReportService
    {
        Task<Result> GenerateReport(NewEmployeeReportModel model);
        Task<Result> DeleteReport(int reportId);
    }
}
