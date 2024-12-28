using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IMembershipPlanService
    {
        Task<IEnumerable<MembershipPlan>> GetPlansAsync();
    }
}