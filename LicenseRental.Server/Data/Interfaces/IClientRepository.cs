using LicenseRental.Shared.Models;

namespace LicenseRental.Server.Data.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetClient(Guid clientId);
        Task<Client> AddClient(Client client);
        IEnumerable<Client> GetAll();
    }
}
