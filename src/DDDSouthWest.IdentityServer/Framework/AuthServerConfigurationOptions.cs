namespace DDDSouthWest.IdentityServer.Framework
{
    public class AuthServerConfigurationOptions
    {
        public IdentityServer IdentityServer { get; set; }
    }

    public class IdentityServer
    {
        public string WebsiteUrl { get; set; }
    }
}