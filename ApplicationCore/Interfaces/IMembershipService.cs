using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    internal interface IMembershipService
    {
        Task<GymMembership> GetActiveMembership(int clientId);
    }
}
