using ApplicationCore.Entities;
using ApplicationCore.Models.Client;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IClientService
    {
        Task<Result> AddClient(NewClientModel model);
        Task<Result> UpdateClient(EditClientModel model);
        Task<Result> DeleteClient(int clientId);
        Task<bool> HasActiveMembership(string userId);
        Task<Client> GetClientByUserId(string userId);
        Task<string> GetClientAccountId(int userId);
    }
}
