namespace LicenseRental.Server.Data
{
    public class DataGenerator
    {
        public static void Initialize(AppDbContext appDbContext)
        {
            appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();
        }       
    }
}
