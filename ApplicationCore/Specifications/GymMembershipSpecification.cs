using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
