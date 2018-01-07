namespace DDDSouthWest.Domain
{
    public class ClientConfigurationOptions
    {
        public IdentityServer IdentityServer { get; set; }

        public Database Database { get; set; }

        public WebsiteSettings WebsiteSettings { get; set; }
    }

    public class IdentityServer
    {
        public string AuthorityServer { get; set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }

    public class WebsiteSettings
    {
        public bool AllowRegistration { get; set; }

        public bool RequireNewAccountConfirmation { get; set; }
    }
}