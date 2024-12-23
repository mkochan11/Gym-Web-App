using ApplicationCore.Entities;
using ApplicationCore.Models.Membership;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMembershipService
    {
        Task<GymMembership> GetActiveMembership(int clientId);
        Task<Result> AddGymMembership(NewMembershipModel model);
    }
}
