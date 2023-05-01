using LicenseRental.Server.Data.Interfaces;
using LicenseRental.Server.Services.Interfaces;
using LicenseRental.Shared.Models;

namespace LicenseRental.Server.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task<Client> AddClientAsync(Guid clientId)
        {
            var clients = _clientRepository.GetAll();
            var client = new Client
            {
                Id = clientId,
                Name = "Client" + (clients.Count() + 1),
            };
            return _clientRepository.AddClient(client);
        }

        public Task<Client> GetClient(Guid clientId)
        {
            return _clientRepository.GetClient(clientId);
        }
    }
}
