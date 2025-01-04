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
}
