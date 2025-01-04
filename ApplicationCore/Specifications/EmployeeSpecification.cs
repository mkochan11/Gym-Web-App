using ApplicationCore.Entities;
using ApplicationCore.Entities.Abstract;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindEmployeeByUserId<T> : SingleResultSpecification<T> where T : Employee
    {
        public FindEmployeeByUserId(string userId)
        {
            Query.Where(x => x.AccountId.ToString() == userId.ToString()).AsSplitQuery();
        }
    }
}
