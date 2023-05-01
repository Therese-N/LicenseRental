using LicenseRental.Shared.Models;

namespace LicenseRental.Server.Data.Interfaces
{
    public interface ILicenseRepository
    {
        Task<IQueryable<License>> GetLicenses();
        Task<License?> GetLicense(int licenseId);
        Task<License> AddLicense(License license);
        Task<License?> UpdateLicense(License license, Guid renterId);
        Task<License?> DeleteLicense(int licenseId);
        Task<License?> GetLicenseByClientId(Guid renterId);
    }
}
