using ApplicationCore.Entities;
using ApplicationCore.Models.Membership;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IMembershipService
    {
        Task<GymMembership> GetActiveMembership(int clientId);
        Task<Result> AddGymMembership(NewMembershipModel model);
    }
}
