using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IRepository<GymMembership> _membershipRepository;

        public MembershipService(IRepository<GymMembership> membershipRepository)
        {
            _membershipRepository = membershipRepository;
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


        private bool IsMembershipActive(GymMembership membership) {
            DateTime currentDate = DateTime.Now;

            return membership.StartDate < currentDate && membership.EndDate > currentDate;
        }
    }
}
