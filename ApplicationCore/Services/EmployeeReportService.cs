using ApplicationCore.Entities;
using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.EmployeeReport;
using ApplicationCore.Specifications;
using Ardalis.Result;

namespace ApplicationCore.Services
{
    public class EmployeeReportService : IEmployeeReportService
    {
        private readonly IRepository<IndividualTraining> _individualTrainingRepository;
        private readonly IRepository<GroupTraining> _groupTrainingRepository;
        private readonly IRepository<PersonalTrainer> _personalTrainerRepository;
        private readonly IRepository<GroupTrainer> _groupTrainerRepository;
        private readonly IRepository<Shift> _shiftRepository;
        private readonly IRepository<Receptionist> _receptionistRepository;
        private readonly IRepository<EmployeeReport> _employeeReportRepository;
        private readonly IRepository<Manager> _managerRepository;

        public EmployeeReportService(IRepository<IndividualTraining> individualTrainingRepository,
            IRepository<GroupTraining> groupTrainingRepository,
            IRepository<PersonalTrainer> personalTrainerRepository,
            IRepository<GroupTrainer> groupTrainerRepository,
            IRepository<Shift> shiftRepository,
            IRepository<Receptionist> receptionistRepository,
            IRepository<EmployeeReport> employeeReportRepository,
            IRepository<Manager> managerRepository)
        {
            _individualTrainingRepository = individualTrainingRepository;
            _groupTrainingRepository = groupTrainingRepository;
            _personalTrainerRepository = personalTrainerRepository;
            _groupTrainerRepository = groupTrainerRepository;
            _shiftRepository = shiftRepository;
            _receptionistRepository = receptionistRepository;
            _employeeReportRepository = employeeReportRepository;
            _managerRepository = managerRepository;
        }

        public async Task<Result> DeleteReport(int reportId)
        {
            var report = await _employeeReportRepository.GetByIdAsync(reportId);

            if (report == null)
            {
                return Result.Error("Nie znaleziono raportu");
            }

            await _employeeReportRepository.DeleteAsync(report);
            return Result.Success();
        }

        public async Task<Result> GenerateReport(NewEmployeeReportModel model)
        {
            var manager = await _managerRepository.FirstOrDefaultAsync(new FindEmployeeByUserId<Manager>(model.ManagerId));

            if (manager == null)
            {
                return Result.Error("Nie znaleziono menadżera");
            }

            var report = new EmployeeReport
            {
                ManagerId = manager.Id,
                EmployeeId = model.EmployeeId,
                Position = model.Position,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                Name = model.Name,
                TotalHoursWorked = 0,
                TotalSalary = 0
            };

            if(model.Position == Position.Receptionist)
            {
                var employee = await _receptionistRepository.GetByIdAsync(model.EmployeeId);

                if (employee == null)
                {
                    return Result.Error("Nie znaleziono pracownika");
                }

                var shifts = await _shiftRepository.ListAsync(new FindShiftsForReceptionistInPeriod(employee.Id, model.FromDate, model.ToDate));

                foreach(var shift in shifts)
                {
                    report.TotalHoursWorked += (decimal)(shift.EndTime - shift.StartTime).TotalHours;
                    report.TotalSalary += (decimal)(shift.EndTime - shift.StartTime).TotalHours * (decimal)employee.Salary;
                }
            }
            else if(model.Position == Position.PersonalTrainer)
            {
                var employee = await _personalTrainerRepository.GetByIdAsync(model.EmployeeId);

                if (employee == null)
                {
                    return Result.Error("Nie znaleziono pracownika");
                }

                var individualTrainings = await _individualTrainingRepository.ListAsync(new FindTrainingsForPersonalTrainerInPeriod(employee.Id, model.FromDate, model.ToDate));

                foreach (var training in individualTrainings)
                {
                    report.TotalHoursWorked += (decimal)(training.Duration).TotalHours;
                    report.TotalSalary += (decimal)(training.Duration).TotalHours * (decimal)employee.Salary;
                }
            }
            else if (model.Position == Position.GroupTrainer)
            {
                var employee = await _groupTrainerRepository.GetByIdAsync(model.EmployeeId);

                if (employee == null)
                {
                    return Result.Error("Nie znaleziono pracownika");
                }

                var groupTrainings = await _groupTrainingRepository.ListAsync(new FindTrainingsForGroupTrainerInPeriod(employee.Id, model.FromDate, model.ToDate));

                foreach (var training in groupTrainings)
                {
                    report.TotalHoursWorked += (decimal)(training.Duration).TotalHours;
                    report.TotalSalary += (decimal)(training.Duration).TotalHours * (decimal)employee.Salary;
                }
            }

            await _employeeReportRepository.AddAsync(report);
            return Result.Success();
        }
    }
}
