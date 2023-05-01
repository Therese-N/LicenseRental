using LicenseRental.Server.Services.Interfaces;
using LicenseRental.Shared.Models;

namespace LicenseRental.Server.Services
{

    class PeriodicHostedService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(1);
        private readonly ILogger<PeriodicHostedService> _logger;
        private readonly IServiceScopeFactory _factory;
        private List<License> _expiredLicenses = new List<License>();
        private int _executionCount = 0;
        public bool IsEnabled { get; set; }

        public PeriodicHostedService(
            ILogger<PeriodicHostedService> logger,
            IServiceScopeFactory factory)
        {
            _logger = logger;
            _factory = factory;
            IsEnabled= true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    if (IsEnabled)
                    {
                        await using AsyncServiceScope asyncScope = _factory.CreateAsyncScope();
                        ILicenseService licenseService = asyncScope.ServiceProvider.GetRequiredService<ILicenseService>();
                        var expiredLicenses = licenseService.GetExpiredLicenses();
                        var newlyExpiredLicenses = expiredLicenses.Where(x => !_expiredLicenses.Any(y => x.Id == y.Id && x.LicenseRenter.ExpirationDate == y.LicenseRenter.ExpirationDate)).ToList();
                        _expiredLicenses.AddRange(newlyExpiredLicenses);
                        newlyExpiredLicenses.ForEach(x => _logger.LogInformation(
                            $"{DateTime.Now}: License with name {x.Name} expired {x.LicenseRenter.ExpirationDate}"));
                
                    }
                    else
                    {
                        _logger.LogInformation(
                            "Skipped PeriodicHostedService");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(
                        $"Failed to execute PeriodicHostedService with exception message {ex.Message}. Good luck next round!");
                }
            }
        }
    }
}
