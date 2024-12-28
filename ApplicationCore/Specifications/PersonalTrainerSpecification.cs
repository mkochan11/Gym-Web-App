using ApplicationCore.Entities;
using Ardalis.Specification;

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