using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.GymReport;
using ApplicationCore.Specifications;
using Ardalis.Result;

namespace ApplicationCore.Services
{
    public class GymReportService : IGymReportService
    {
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IRepository<GymReport> _gymReportRepository;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<GymMembership> _membershipRepository;
        private readonly IRepository<MembershipPlan> _membershipPlanRepository;
        private readonly IRepository<IndividualTraining> _individualTrainingRepository;
        private readonly IRepository<GroupTraining> _groupTrainingRepository;
        private readonly IRepository<PersonalTrainer> _personalTrainerRepository;
        private readonly IRepository<GroupTrainer> _groupTrainerRepository;
        private readonly IRepository<Shift> _shiftRepository;
        private readonly IRepository<Receptionist> _receptionistRepository;

        public GymReportService(IRepository<Owner> ownerRepository, 
            IRepository<GymReport> gymReportRepository, 
            IRepository<Client> clientRepository, 
            IRepository<GymMembership> membershipRepository, 
            IRepository<MembershipPlan> membershipPlanRepository, 
            IRepository<IndividualTraining> individualTrainingRepository, 
            IRepository<GroupTraining> groupTrainingRepository, 
            IRepository<PersonalTrainer> personalTrainerRepository, 
            IRepository<GroupTrainer> groupTrainerRepository, 
            IRepository<Shift> shiftRepository,
            IRepository<Receptionist> receptionistRepository)
        {
            _ownerRepository = ownerRepository;
            _gymReportRepository = gymReportRepository;
            _clientRepository = clientRepository;
            _membershipRepository = membershipRepository;
            _membershipPlanRepository = membershipPlanRepository;
            _individualTrainingRepository = individualTrainingRepository;
            _groupTrainingRepository = groupTrainingRepository;
            _personalTrainerRepository = personalTrainerRepository;
            _groupTrainerRepository = groupTrainerRepository;
            _shiftRepository = shiftRepository;
            _receptionistRepository = receptionistRepository;
        }

        public async Task<Result> DeleteRaport(int id)
        {
            var raport = await _gymReportRepository.GetByIdAsync(id);

            if (raport == null)
            {
                return Result.Error("Nie znaleziono raportu");
            }

            await _gymReportRepository.DeleteAsync(raport);
            return Result.Success();
        }

        public async Task<Result> GenerateReport(NewGymReportModel model)
        {
            var owner = await _ownerRepository.FirstOrDefaultAsync(new FindEmployeeByUserId<Owner>(model.UserId));
            if (owner == null)
            {
                return Result.Error("Nie znaleziono użytkownika");
            }

            var report = new GymReport
            {
                OwnerId = owner.Id,
                Name = model.Name,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                BudgetReport = model.BudgetReport,
                ClientsReport = model.ClientsReport,
                IndividualTrainingsReport = model.IndividualTrainingsReport,
                GroupTrainingsReport = model.GroupTrainingsReport
            };

            if (model.ClientsReport)
            {
                report.NewClients = await _clientRepository.CountAsync(new FindRegisteredClientInPeriod(model.FromDate, model.ToDate));
                report.NewMemberships = await _membershipRepository.CountAsync(new FindNewMembershipsInPeriod(model.FromDate, model.ToDate));
            }

            if (model.BudgetReport)
            {
                report.TotalIncome = 0;
                report.TotalEmployeesCost = 0;
                var newMemberships = await _membershipRepository.ListAsync(new FindNewMembershipsInPeriod(model.FromDate, model.ToDate));
                foreach (var membership in newMemberships)
                {
                    var plan = await _membershipPlanRepository.GetByIdAsync(membership.MembershipPlanId);
                    report.TotalIncome += plan.Price;
                }

                var shifts = await _shiftRepository.ListAsync(new FindShiftsInPeriod(model.FromDate, model.ToDate));
                foreach (var shift in shifts)
                {
                    var receptionist = await _receptionistRepository.GetByIdAsync(shift.ReceptionistId);
                    var shiftDuration = (shift.EndTime - shift.StartTime).TotalHours;
                    report.TotalEmployeesCost += (decimal)receptionist.Salary * (decimal)shiftDuration;
                }

                var individualTrainings = await _individualTrainingRepository.ListAsync(new FindTrainingsInPeriod<IndividualTraining>(model.FromDate, model.ToDate));
                foreach (var training in individualTrainings)
                {
                    var trainer = await _personalTrainerRepository.GetByIdAsync(training.PersonalTrainerId);
                    report.TotalEmployeesCost += (decimal)trainer.Salary * (decimal)training.Duration.TotalHours;
                }

                var groupTrainings = await _groupTrainingRepository.ListAsync(new FindTrainingsInPeriod<GroupTraining>(model.FromDate, model.ToDate));
                foreach (var training in groupTrainings)
                {
                    var trainer = await _groupTrainerRepository.GetByIdAsync(training.GroupTrainerId);
                    report.TotalEmployeesCost += (decimal)trainer.Salary * (decimal)training.Duration.TotalHours;
                }
            }

            if (model.IndividualTrainingsReport)
            {
                report.TotalIndividualTrainingsTime = TimeSpan.Zero;
                var individualTrainings = await _individualTrainingRepository.ListAsync(new FindTrainingsInPeriod<IndividualTraining>(model.FromDate, model.ToDate));
                report.TotalIndividualTrainings = individualTrainings.Count;
                foreach (var training in individualTrainings)
                {
                    report.TotalIndividualTrainingsTime += training.Duration;
                }
            }

            if (model.GroupTrainingsReport)
            {
                report.TotalGroupTrainingsTime = TimeSpan.Zero;
                var groupTrainings = await _groupTrainingRepository.ListAsync(new FindTrainingsInPeriod<GroupTraining>(model.FromDate, model.ToDate));
                report.TotalGroupTrainings = groupTrainings.Count;
                foreach (var training in groupTrainings)
                {
                    report.TotalGroupTrainingsTime += training.Duration;
                }
            }

            await _gymReportRepository.AddAsync(report);
            return Result.Success();
        }
    }
}
