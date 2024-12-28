using ApplicationCore.Entities;
using ApplicationCore.Models.Client;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IClientService
    {
        Task<Result> AddClient(NewClientModel newClient);
        Task<bool> HasActiveMembership(string userId);
        Task<Client> GetClientByUserId(string userId);
    }
}
