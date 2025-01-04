using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindClientByUserId : SingleResultSpecification<Client>
    {
        public FindClientByUserId(string userId)
        {
            Query.Where(x => x.AccountId.ToString() == userId.ToString()).AsSplitQuery();
        }
    }

    public class FindRegisteredClientInPeriod : SingleResultSpecification<Client>
    {
        public FindRegisteredClientInPeriod(DateTime fromDate, DateTime toDate)
        {
            Query.Where(x => x.RegistrationDate >= fromDate && x.RegistrationDate <= toDate).AsSplitQuery();
        }
    }
}
