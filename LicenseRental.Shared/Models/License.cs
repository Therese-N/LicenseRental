namespace LicenseRental.Shared.Models
{
    public class License
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Status
        {
            get
            {
                if(LicenseRenter != null && LicenseRenter.ExpirationDate.Subtract(DateTime.Now).Seconds > 0)
                {
                    return LicenseRenter.Client.Name + ", " + LicenseRenter.ExpirationDate.Subtract(DateTime.Now).Seconds + " seconds left";
                }
                return "not rented";
            }
            set { }
        }
        public DateTime? ExpirationDate {
            get
            {
                if (LicenseRenter != null && LicenseRenter.ExpirationDate ! != null)
                {
                    return LicenseRenter.ExpirationDate;
                }

                return null;
            }
            set { }

        }
        public LicenseRenter? LicenseRenter { get; set; }
    }
}