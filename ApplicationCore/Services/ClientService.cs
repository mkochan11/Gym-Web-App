using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Client;
using ApplicationCore.Specifications;
using Ardalis.Result;

namespace ApplicationCore.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientRepository;
        private readonly IMembershipService _membershipService;
        public ClientService(
            IRepository<Client> clientRepository,
            IMembershipService membershipService)
        {
            _clientRepository = clientRepository;
            _membershipService = membershipService;
        }

        /// <summary>
        /// Adds new Client
        /// </summary>
        /// <param name="newClient"></param>
        /// <returns>A <see cref="Result"/> indicating the success or failure of the operation.</returns>
        public async Task<Result> AddClient(NewClientModel newClient)
        {
            Client client = new Client
            {
                AccountId = newClient.AccountId,
                Name = newClient.Name,
                Surname = newClient.Surname,
                RegistrationDate = DateTime.Now,
            };
            
            await _clientRepository.AddAsync(client);

            return Result.Success();
        }

        /// <summary>
        /// Checks if Client has acitve GymMembership
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// Returns <c>true</c> if an active membership is found for the client, otherwise <c>false</c>.</returns>
        public async Task<bool> HasActiveMembership(string userId)
        {
            var client = await GetClientByUserId(userId);

            var result = await _membershipService.GetActiveMembership(client.Id);

            return result != null;
        }

        /// <summary>
        /// Gets Client by user Id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns><see cref="Client"/> entity.</returns>
        public async Task<Client> GetClientByUserId(string userId){
            var _clientSpec = new FindClientByUserId(userId);
            var client = await _clientRepository.FirstOrDefaultAsync(_clientSpec);

            return client;
        }

        public async Task<string> GetClientAccountId(int userId)
        {
            var user = await _clientRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return "";
            }

            return user.AccountId;
        }

        public async Task<Result> UpdateClient(EditClientModel model)
        {
            var client = await _clientRepository.GetByIdAsync(model.Id);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            client.Name = model.Name;
            client.Surname = model.Surname;

            await _clientRepository.UpdateAsync(client);
            return Result.Success();
        }

        public async Task<Result> DeleteClient(int clientId)
        {
            var client = await _clientRepository.GetByIdAsync(clientId);

            if (client == null)
            {
                return Result.Error("Nie znaleziono klienta");
            }

            await _clientRepository.DeleteAsync(client);
            return Result.Success();
        }
    }
}