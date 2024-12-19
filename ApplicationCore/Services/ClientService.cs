using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Models.Client;
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
        public ClientService(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }
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
    }
}
