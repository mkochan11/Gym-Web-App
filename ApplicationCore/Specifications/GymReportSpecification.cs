using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindGymReportByOwnerId : SingleResultSpecification<GymReport>
    {
        public FindGymReportByOwnerId(int ownerId)
        {
            Query.Where(x => x.OwnerId == ownerId).AsSplitQuery();
        }
    }
}
