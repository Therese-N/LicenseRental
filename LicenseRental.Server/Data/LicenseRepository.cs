using LicenseRental.Server.Data.Interfaces;
using LicenseRental.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LicenseRental.Server.Data
{
    public class LicenseRepository : ILicenseRepository
    {
        private readonly AppDbContext _appDbContext;

        public LicenseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<License> AddLicense(License license)
        {
            var result = await _appDbContext.Licenses.AddAsync(license);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public Task<License?> DeleteLicense(int licenseId) => throw new NotImplementedException();
        public Task<License?> GetLicense(int licenseId) => throw new NotImplementedException();
        public Task<License> GetLicenseByClientId(Guid renterId) 
        {
            return _appDbContext.Licenses.Where(x => x.LicenseRenter != null && x.LicenseRenter.Client.Id == renterId)
                .OrderByDescending(x=>x.LicenseRenter.ExpirationDate)
                .Include(x=>x.LicenseRenter)
                .Include(x=>x.LicenseRenter.Client)
                .FirstOrDefaultAsync();
        }

        public async Task<IQueryable<License>> GetLicenses()
        {
            return _appDbContext.Licenses.Include(x => x.LicenseRenter.Client);
        }
        public async Task<License?> UpdateLicense(License license, Guid renterId)
        {
            var licenseToUpdate = await _appDbContext.Licenses.Include(x=>x.LicenseRenter).FirstOrDefaultAsync(x => x.Id == license.Id);

            var renter = _appDbContext.Clients.FirstOrDefault(x => x.Id == renterId);
            if (licenseToUpdate != null)
            {
                var licenseRenter = new LicenseRenter { 
                    Id = new Guid(),
                    Client = renter,
                    LicenseId = licenseToUpdate.Id,
                    ExpirationDate = DateTime.Now.AddSeconds(15),
                }; 
                _appDbContext.Add(licenseRenter);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            return licenseToUpdate;
        }
    }
}
