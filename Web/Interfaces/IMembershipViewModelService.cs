using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.Membership;

namespace Web.Interfaces
{
    public interface IMembershipViewModelService
    {
        Task<MembershipIndexViewModel?> GetMembershipIndexViewModel(string UserId);
    }
}