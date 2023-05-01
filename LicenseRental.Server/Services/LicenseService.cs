using LicenseRental.Server.Data.Interfaces;
using LicenseRental.Server.Services.Interfaces;
using LicenseRental.Shared.Models;

namespace LicenseRental.Server.Services
{
    public class LicenseService : ILicenseService
    {
        private readonly ILicenseRepository _licenseRepository;
        private readonly ILogger<LicenseService> _logger;
        public LicenseService(ILicenseRepository licenseRepository, ILogger<LicenseService> logger)
        {
            _licenseRepository = licenseRepository;
            _logger = logger;
        }
        public Task<License> AddLicenseAsync(string name)
        {
            var licenses = _licenseRepository.GetLicenses();
            var licenseNames = licenses.Result.Select(x => x.Name).ToList();
            if (string.IsNullOrEmpty(name) || licenseNames.Contains(name))
            {
                throw new Exception("License name must be an unique alphanumeric string!");
            }

            var license = new License
            {
                Id = new Guid(),
                Name = name,
            };
            _logger.LogInformation($"Added license with name {license.Name}");
            return _licenseRepository.AddLicense(license);
        }

        public Task<License> GetLicenseByClientId(Guid clientId)
        {
            return _licenseRepository.GetLicenseByClientId(clientId);
        }

        public async Task<IEnumerable<License>> GetLicenses() 
        {
            return await _licenseRepository.GetLicenses();
        }
        public async Task<License> UpdateLicenseAsync(Guid renterId) 
        {
            var licenses = GetLicenses();
            var clientLicense = _licenseRepository.GetLicenseByClientId(renterId).Result;
            if(clientLicense != null && !clientLicense.Status.Equals("not rented"))
            {
                throw new Exception("Client already have an active license");
            }
            
            var licenseForRent = licenses.Result.Where(x => x.LicenseRenter == null || x.Status.Equals("not rented")).FirstOrDefault();
            if (licenseForRent != null)
            {
                _logger.LogInformation($"{DateTime.Now}: License with name {licenseForRent.Name} was rented by clientId {renterId}.");
                return await _licenseRepository.UpdateLicense(licenseForRent, renterId);
            }

            throw new Exception("There are no licenses to rent!");
        }

        public IEnumerable<License> GetExpiredLicenses() 
        {
            var licenses = _licenseRepository.GetLicenses().Result.ToList();
            return licenses.Where(x => x.LicenseRenter?.ExpirationDate < DateTime.Now).ToList();
        }
    }
}
