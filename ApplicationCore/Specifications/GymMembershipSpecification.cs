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
}
