using Web.ViewModels.Membership;

namespace Web.Interfaces
{
    public interface IMembershipViewModelService
    {
        Task<MembershipIndexViewModel?> GetMembershipIndexViewModel(string UserId);
    }
}