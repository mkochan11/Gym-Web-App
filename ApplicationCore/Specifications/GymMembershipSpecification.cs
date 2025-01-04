using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindMembershipByClientId : SingleResultSpecification<GymMembership>
    {
        public FindMembershipByClientId(int clientId)
        {
            Query.Where(x => x.ClientId == clientId).AsSplitQuery();
        }
    }

    public class FindNewMembershipsInPeriod : SingleResultSpecification<GymMembership>
    {
        public FindNewMembershipsInPeriod(DateTime fromDate, DateTime toDate)
        {
            Query.Where(x => x.StartDate >= fromDate && x.StartDate <= toDate)
                .AsSplitQuery();
        }
    }
}
