using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindGroupTrainerByUserId : SingleResultSpecification<GroupTrainer>
    {
        public FindGroupTrainerByUserId(string userId)
        {
            Query.Where(x => x.AccountId.ToString() == userId.ToString()).AsSplitQuery();
        }
    }
}
