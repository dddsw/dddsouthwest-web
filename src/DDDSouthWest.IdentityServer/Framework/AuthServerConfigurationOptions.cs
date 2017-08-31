namespace DDDSouthWest.IdentityServer.Framework
{
    public class AuthServerConfigurationOptions
    {
        public string WebsiteUrl { get; set; }

        public Database Database { get; set; }
    }

    public class Database
    {
        public string Type { get; set; }

        public string ConnectionString { get; set; }
    }
}