using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class FindPersonalTrainerByUserId : SingleResultSpecification<PersonalTrainer>
    {
        public FindPersonalTrainerByUserId(string userId)
        {
            Query.Where(x => x.AccountId.ToString() == userId.ToString()).AsSplitQuery();
        }
    }
}
