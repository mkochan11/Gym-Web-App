using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IMembershipPlanService
    {
        Task<IEnumerable<MembershipPlan>> GetPlansAsync();
    }
}