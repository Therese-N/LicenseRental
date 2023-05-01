using LicenseRental.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace LicenseRental.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<License> Licenses => Set<License>();
        public DbSet<LicenseRenter> LicenseRenters => Set<LicenseRenter>();
        public DbSet<Client> Clients => Set<Client>();
    }
}
