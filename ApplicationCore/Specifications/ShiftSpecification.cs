using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindShiftsInPeriod : SingleResultSpecification<Shift>
    {
        public FindShiftsInPeriod(DateTime fromDate, DateTime toDate)
        {
            Query.Where(shift => shift.StartTime >= fromDate && shift.EndTime <= toDate);
        }
    }

    public class FindShiftsForReceptionistInPeriod : SingleResultSpecification<Shift>
    {
        public FindShiftsForReceptionistInPeriod(int receptionistId, DateTime fromDate, DateTime toDate)
        {
            Query.Where(shift => shift.ReceptionistId == receptionistId && shift.StartTime >= fromDate && shift.EndTime <= toDate);
        }
    }

    public class FindShiftsByMonth : SingleResultSpecification<Shift>
    {
        public FindShiftsByMonth(int month, int year)
        {
            Query.Where(shift => shift.StartTime.Month == month && shift.StartTime.Year == year);
        }
    }
}
