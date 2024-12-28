using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Web.Interfaces;
using Web.ViewModels.Membership.Buy;

namespace Web.Services
{
    public class MembershipBuyViewModelService : IMembershipBuyViewModelService
    {
        private readonly IRepository<MembershipPlan> _membershipPlanRepository;

        public MembershipBuyViewModelService(IRepository<MembershipPlan> membershipPlanRepository)
        {
            _membershipPlanRepository = membershipPlanRepository;
        }
        public async Task<MembershipBuyViewModel> GetMembershipBuyViewModel()
        {
            var membershipPlans = await _membershipPlanRepository.ListAsync();

            if (membershipPlans.Any())
            {
                MembershipBuyViewModel viewModel = new MembershipBuyViewModel
                {
                    Plans = membershipPlans.Select(plan => new MembershipBuyPlanItemViewModel
                    {
                        Id = plan.Id,
                        Type = plan.Type,
                        Description = plan.Description,
                        Price = plan.Price,
                        DurationTime = plan.DurationTime,
                    }).ToList(),
                    IsFound = true
                };

                return viewModel;

            }
            else
            {
                MembershipBuyViewModel viewModel = new MembershipBuyViewModel
                {
                    Plans = new List<MembershipBuyPlanItemViewModel>(),
                    IsFound = false
                };

                return viewModel;
            }
        }
    }
}
