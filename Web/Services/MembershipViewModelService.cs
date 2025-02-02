using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Web.Interfaces;
using Web.ViewModels.Membership;

namespace Web.Services
{
    public class MembershipViewModelService : IMembershipViewModelService   
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<GymMembership> _membershipRepository;
        private readonly IRepository<MembershipPlan> _membershipPlanRepository;

        public MembershipViewModelService(
            IRepository<Client> clientRepository,
            IRepository<GymMembership> membershipRepository,
            IRepository<MembershipPlan> membershipPlanRepository
        )
        {
            _clientRepository = clientRepository;
            _membershipRepository = membershipRepository;
            _membershipPlanRepository = membershipPlanRepository;
        }

        public async Task<MembershipIndexViewModel?> GetMembershipIndexViewModel(string UserId)
        {
            var client = await _clientRepository.SingleOrDefaultAsync(new FindClientByUserId(UserId));;

            var _membershipSpec = new FindMembershipByClientId(client.Id);
            var memberships = await _membershipRepository.ListAsync(_membershipSpec);
            if (memberships.Count > 0){
                var latestMembership = memberships.OrderByDescending(m => m.EndDate).FirstOrDefault();

                latestMembership.MembershipPlan = await _membershipPlanRepository.GetByIdAsync(latestMembership.MembershipPlanId);

                var membershipIndexItemViewModel = new MembershipIndexItemViewModel
                {
                    Id = latestMembership.Id,
                    StartDate = latestMembership.StartDate,
                    EndDate = latestMembership.EndDate,
                    PlanType = latestMembership.MembershipPlan.Type,
                    PlanDescription = latestMembership.MembershipPlan.Description,
                    Price = latestMembership.MembershipPlan.Price,
                    Status = latestMembership.EndDate < DateTime.Today ? "Zakończony" : "Aktywny"
                };

                var membershipIndexViewModel = new MembershipIndexViewModel
                {
                    MembershipIndexItem = membershipIndexItemViewModel,
                    IsFound = true,
                };
                return membershipIndexViewModel;
            }
            else
            {
                return new MembershipIndexViewModel { IsFound = false };

            }
        }
    }
}