using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using Ardalis.Result;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models.Membership;

namespace ApplicationCore.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IRepository<GymMembership> _membershipRepository;
        private readonly IRepository<MembershipPlan> _membershipPlanRepository;
        private readonly IRepository<Client> _clientRepository;
        //private readonly IClientService _clientService;

        public MembershipService(
            IRepository<GymMembership> membershipRepository,
            IRepository<MembershipPlan> membershipPlanRepository,
            IRepository<Client> clientRepository
            //IClientService clientService
            )
        {
            _membershipRepository = membershipRepository;
            //_clientService = clientService;
            _membershipPlanRepository = membershipPlanRepository;
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// Gets Client's active GymMembership
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>
        /// A <see cref="GymMembership"/> object representing the active membership of the client, 
        /// or <c>null</c> if no active membership is found.
        /// </returns>
        public async Task<GymMembership> GetActiveMembership(int clientId)
        {
            var _membershipSpec = new FindMembershipByClientId(clientId);
            var clientMemberships = await _membershipRepository.ListAsync(_membershipSpec);

            GymMembership activeMembership = null;

            foreach (var clientMembership in clientMemberships)
            {
                if (IsMembershipActive(clientMembership))
                {
                    activeMembership = clientMembership;
                }
            }
            return activeMembership;
        }

        /// <summary>
        /// Determines if the specified gym membership is currently active.
        /// </summary>
        /// <param name="membership">The gym membership to check.</param>
        /// <returns><c>true</c> if the membership is active; otherwise, <c>false</c>.</returns>
        public bool IsMembershipActive(GymMembership membership) {
            DateTime currentDate = DateTime.Now;
            return membership.StartDate < currentDate && membership.EndDate > currentDate;
        }

        /// <summary>
        /// Adds new GymMembership to the database with Client and MembershipPlan details
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
        public async Task<Result> AddGymMembership(NewMembershipModel model)
        {
            var _clientSpec = new FindClientByUserId(model.UserId);
            var client = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            if(client == null)
            {
                return Result.Error("Client not found");
            }

            var membershipPlan = await _membershipPlanRepository.GetByIdAsync(model.MembershipPlanId);

            if(membershipPlan == null)
            {
                return Result.Error("Membership plan not found");
            }

            GymMembership membership = new GymMembership{
                Client = client,
                MembershipPlan = membershipPlan,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(membershipPlan.DurationInMonths),
            };

            var result = await _membershipRepository.AddAsync(membership);
            
            if(result == null){
                return Result.Error("Membership could not be added");
            }

            return Result.Success();
        }
    }
}
