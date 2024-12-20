using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class FindClientByUserId : SingleResultSpecification<Client>
    {
        public FindClientByUserId(string userId)
        {
            Query.Where(x => x.AccountId == userId).AsSplitQuery();
        }
    }
}
