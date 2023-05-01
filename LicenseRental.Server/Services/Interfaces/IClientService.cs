using LicenseRental.Shared.Models;

namespace LicenseRental.Server.Services.Interfaces
{
    public interface IClientService
    {
        Task<Client> GetClient(Guid clientId);
        Task<Client> AddClientAsync(Guid clientId);
    }
}
