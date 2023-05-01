namespace LicenseRental.Client.Config
{
    public interface ISessionIdProvider
    {
        Guid SessionId { get; }
    }

    public class SessionIdProvider : ISessionIdProvider
    {
        public Guid SessionId { get; } = Guid.NewGuid();
    }
}
