
using ApplicationCore.Models.GymReport;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IGymReportService
    {
        Task<Result> GenerateReport(NewGymReportModel model);
        Task<Result> DeleteRaport(int id);
    }
}
