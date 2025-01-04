using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApplicationCore.Specifications
{
    public class FindEmployeeReportByManagerId : SingleResultSpecification<EmployeeReport>
    {
        public FindEmployeeReportByManagerId(int employeeId)
        {
            Query.Where(e => e.ManagerId == employeeId).AsSplitQuery();
        }
    }
}
