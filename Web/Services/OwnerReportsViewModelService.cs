using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Web.Interfaces;
using Web.ViewModels.Owner.Reports;

namespace Web.Services
{
    public class OwnerReportsViewModelService : IOwnerReportsViewModelService
    {
        private readonly IRepository<GymReport> _gymReportRepository;
        private readonly IRepository<Owner> _ownerRepository;

        public OwnerReportsViewModelService(IRepository<GymReport> gymReportRepository, IRepository<Owner> ownerRepository)
        {
            _gymReportRepository = gymReportRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task<OwnerReportsIndexViewModel> GetOwnerReportsIndexViewModel(string userId)
        {
            var owner = await _ownerRepository.FirstOrDefaultAsync(new FindEmployeeByUserId<Owner>(userId));
            var gymReports = await _gymReportRepository.ListAsync(new FindGymReportByOwnerId(owner.Id));
            
            var viewModel = new OwnerReportsIndexViewModel
            {
                Reports = gymReports.Select(r => new OwnerReportsIndexItemViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    FromDate = r.FromDate,
                    ToDate = r.ToDate,
                    ClientsReport = r.ClientsReport,
                    BudgetReport = r.BudgetReport,
                    IndividualTrainingsReport = r.IndividualTrainingsReport,
                    GroupTrainingsReport = r.GroupTrainingsReport,
                    NewClients = r.NewClients ?? null,
                    NewMemberships = r.NewMemberships ?? null,
                    TotalIncome = r.TotalIncome ?? null,
                    TotalEmployeesCost = r.TotalEmployeesCost ?? null,
                    TotalIndividualTrainings = r.TotalIndividualTrainings ?? null,
                    TotalGroupTrainings = r.TotalGroupTrainings ?? null,
                    TotalIndividualTrainingsTime = r.TotalIndividualTrainingsTime ?? null,
                    TotalGroupTrainingsTime = r.TotalGroupTrainingsTime ?? null,
                    TotalProfit = (r.TotalIncome ?? 0) - (r.TotalEmployeesCost ?? 0)
                }).ToList()
            };

            return viewModel;
        }
    }
}