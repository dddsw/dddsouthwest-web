namespace DDDSouthWest.Website.Framework
{
    // TODO: Move auth config from Domain.ClientConfigurationOptions to here?
    public class AuthServerConfigurationOptions
    {
        public IdentityServer IdentityServer { get; set; }
    }

    public class IdentityServer
    {
        public string AuthorityServer { get; set; }
    }
}