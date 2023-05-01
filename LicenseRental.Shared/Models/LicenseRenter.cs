namespace LicenseRental.Shared.Models
{
    public class LicenseRenter
    {
        public Guid Id { get; set; }
        public Guid LicenseId { get; set; }
        public Client? Client { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
