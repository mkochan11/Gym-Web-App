using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Client;
using ApplicationCore.Specifications;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var _clientSpec = new FindClientByUserId(userId);
            var client = _clientRepository.FirstOrDefaultAsync(_clientSpec);

            var result = await _membershipService.GetActiveMembership(client.Id);

            return result != null;
        }
    }
}
