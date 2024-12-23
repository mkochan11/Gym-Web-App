using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class MembershipPlanService : IMembershipPlanService
    {
        private readonly IRepository<MembershipPlan> _membershipPlanRepository;

        public MembershipPlanService(IRepository<MembershipPlan> membershipPlanRepository)
        {
            _membershipPlanRepository = membershipPlanRepository;
        }
        public async Task<IEnumerable<MembershipPlan>> GetPlansAsync()
        {
            var plans = await _membershipPlanRepository.ListAsync();

            return plans;
        }
    }
}