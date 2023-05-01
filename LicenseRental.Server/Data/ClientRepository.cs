using LicenseRental.Server.Data.Interfaces;
using LicenseRental.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LicenseRental.Server.Data
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClientRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Client> AddClient(Client client)
        {
            if (!_appDbContext.Clients.Contains(client))
            {
                var result = await _appDbContext.Clients.AddAsync(client);
                _appDbContext.SaveChanges();
                return result.Entity;
            }
            return client;
        }
        public IEnumerable<Client> GetAll() => _appDbContext.Clients.ToList();
        public async Task<Client> GetClient(Guid clientId)
        {
            var result = await _appDbContext.Clients.FirstOrDefaultAsync(x => x.Id == clientId);
            return result ?? new Client();
        }
    }
}
