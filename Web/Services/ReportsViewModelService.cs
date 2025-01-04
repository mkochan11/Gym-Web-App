using ApplicationCore.Entities;
using ApplicationCore.Entities.Abstract;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Web.Interfaces;
using Web.ViewModels.Manager.Reports;
using Web.ViewModels.Owner.Reports;

namespace Web.Services
{
    public class ReportsViewModelService : IReportsViewModelService
    {
        private readonly IRepository<GymReport> _gymReportRepository;
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IRepository<Manager> _managerRepository;
        private readonly IRepository<EmployeeReport> _employeeReportRepository;
        private readonly IRepository<Receptionist> _receptionistRepository;
        private readonly IRepository<PersonalTrainer> _personalTrainerRepository;
        private readonly IRepository<GroupTrainer> _groupTrainerRepository;

        public ReportsViewModelService(IRepository<GymReport> gymReportRepository, 
            IRepository<Owner> ownerRepository,
            IRepository<Manager> managerRepository,
            IRepository<Receptionist> receptionistRepository,
            IRepository<PersonalTrainer> personalTrainerRepository,
            IRepository<GroupTrainer> groupTrainerRepository,
            IRepository<EmployeeReport> employeeReportRepository)
        {
            _gymReportRepository = gymReportRepository;
            _ownerRepository = ownerRepository;
            _managerRepository = managerRepository;
            _receptionistRepository = receptionistRepository;
            _personalTrainerRepository = personalTrainerRepository;
            _groupTrainerRepository = groupTrainerRepository;
            _employeeReportRepository = employeeReportRepository;
        }

        public async Task<ManagerReportsIndexViewModel> GetManagerReportsIndexViewModel(string userId)
        {
            var manager = await _managerRepository.FirstOrDefaultAsync(new FindEmployeeByUserId<Manager>(userId));

            var employeeReports = await _employeeReportRepository.ListAsync(new FindEmployeeReportByManagerId(manager.Id));

            var receptionists = await _receptionistRepository.ListAsync();
            var personalTrainers = await _personalTrainerRepository.ListAsync();
            var groupTrainers = await _groupTrainerRepository.ListAsync();

            var _employeeItems = new List<ManagerReportsEmployeeItemViewModel>();

            foreach (var receptionist in receptionists)
            {
                _employeeItems.Add(new ManagerReportsEmployeeItemViewModel
                {
                    Id = receptionist.Id,
                    Name = receptionist.Name,
                    Surname = receptionist.Surname,
                    Position = receptionist.Position,
                });
            }

            foreach(var personalTrainer in personalTrainers)
            {
                _employeeItems.Add(new ManagerReportsEmployeeItemViewModel
                {
                    Id = personalTrainer.Id,
                    Name = personalTrainer.Name,
                    Surname = personalTrainer.Surname,
                    Position = personalTrainer.Position,
                });
            }

            foreach (var groupTrainer in groupTrainers)
            {
                _employeeItems.Add(new ManagerReportsEmployeeItemViewModel
                {
                    Id = groupTrainer.Id,
                    Name = groupTrainer.Name,
                    Surname = groupTrainer.Surname,
                    Position = groupTrainer.Position,
                });
            }

            var _reportItems = new List<ManagerReportsIndexItemViewModel>();

            foreach(var report in employeeReports)
            {
                var reportItem = new ManagerReportsIndexItemViewModel
                {
                    Id = report.Id,
                    Name = report.Name,
                    FromDate = report.FromDate,
                    ToDate = report.ToDate,
                };

                if (report.Position == Position.Receptionist)
                {
                    var employee = receptionists.FirstOrDefault(e => e.Id == report.EmployeeId);
                    reportItem.EmployeeName = employee.Name + " " + employee.Surname;
                }
                else if (report.Position == Position.PersonalTrainer)
                {
                    var employee = personalTrainers.FirstOrDefault(e => e.Id == report.EmployeeId);
                    reportItem.EmployeeName = employee.Name + " " + employee.Surname;
                }
                else if (report.Position == Position.GroupTrainer)
                {
                    var employee = groupTrainers.FirstOrDefault(e => e.Id == report.EmployeeId);
                    reportItem.EmployeeName = employee.Name + " " + employee.Surname;
                }

                var detailedReport = new ManagerReportsDetailedItemViewModel
                {
                    HoursWorked = report.TotalHoursWorked,
                    MoneyEarned = report.TotalSalary
                };

                reportItem.DetailedReport = detailedReport;

                _reportItems.Add(reportItem);
            }

            var viewModel = new ManagerReportsIndexViewModel
            {
                Reports = _reportItems,
                Employees = _employeeItems
            };

            return viewModel;
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