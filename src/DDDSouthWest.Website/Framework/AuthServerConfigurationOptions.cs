namespace DDDSouthWest.Website.Framework
{
    public class AuthServerConfigurationOptions
    {
        public IdentityServer IdentityServer { get; set; }
    }

    public class IdentityServer
    {
        public string AuthorityServer { get; set; }
    }
}