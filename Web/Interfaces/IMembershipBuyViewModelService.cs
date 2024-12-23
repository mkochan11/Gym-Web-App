using Web.ViewModels.Membership.Buy;

namespace Web.Interfaces
{
    public interface IMembershipBuyViewModelService
    {
        Task<MembershipBuyViewModel> GetMembershipBuyViewModel();
    }
}
