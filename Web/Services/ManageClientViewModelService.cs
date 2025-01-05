using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Web.Interfaces;
using Web.ViewModels.ManageClient;

namespace Web.Services
{
    public class ManageClientViewModelService : IManageClientViewModelService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<MembershipPlan> _membershipPlanRepository;
        private readonly IRepository<GymMembership> _gymMembershipRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageClientViewModelService(IRepository<Client> clientRepository, 
            IRepository<MembershipPlan> membershipPlanRepository, 
            IRepository<GymMembership> gymMembershipRepository, 
            UserManager<ApplicationUser> userManager)
        {
            _clientRepository = clientRepository;
            _membershipPlanRepository = membershipPlanRepository;
            _gymMembershipRepository = gymMembershipRepository;
            _userManager = userManager;
        }

        public async Task<ManageClientIndexViewModel> GetManageClientIndexViewModel(string clientId)
        {
            var client = await _clientRepository.FirstOrDefaultAsync(new FindClientByUserId(clientId));

            var email = (await _userManager.FindByIdAsync(clientId)).Email;

            var membershipPlans = await _membershipPlanRepository.ListAsync();

            var _memberhipPlanItems = new List<ManageClientMembershipPlanItemViewModel>();
            foreach (var membershipPlan in membershipPlans)
            {
                _memberhipPlanItems.Add(new ManageClientMembershipPlanItemViewModel
                {
                    Id = membershipPlan.Id,
                    Type = membershipPlan.Type,
                    Price = membershipPlan.Price,
                    DurationTime = membershipPlan.DurationTime,
                });
            }

            var gymMembership = await _gymMembershipRepository.FirstOrDefaultAsync(new FindMembershipByClientId(client.Id));

            if (gymMembership != null)
            {
                var _membershipItem = new ManageClientMembershipViewModel
                {
                    Id = gymMembership.Id,
                    StartDate = gymMembership.StartDate,
                    EndDate = gymMembership.EndDate,
                    MembershipPlan = _memberhipPlanItems.FirstOrDefault(x => x.Id == gymMembership.MembershipPlanId),
                };

                var _clientItem = new ManageClientClientViewModel
                {
                    Id = client.Id,
                    AccountId = client.AccountId,
                    Name = client.Name,
                    Surname = client.Surname,
                    Email = email,
                    Membership = _membershipItem,
                };

                var viewModel = new ManageClientIndexViewModel
                {
                    Client = _clientItem,
                    MembershipPlans = _memberhipPlanItems,
                };

                return viewModel;
            }
            else
            {
                var _clientItem = new ManageClientClientViewModel
                {
                    Id = client.Id,
                    AccountId = client.AccountId,
                    Name = client.Name,
                    Surname = client.Surname,
                    Email = email,
                };

                var viewModel = new ManageClientIndexViewModel
                {
                    Client = _clientItem,
                    MembershipPlans = _memberhipPlanItems,
                };

                return viewModel;

            }
        }
    }
}
