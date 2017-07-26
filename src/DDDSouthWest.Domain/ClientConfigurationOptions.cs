namespace DDDSouthWest.Domain
{
    public class ClientConfigurationOptions
    {
        public Database Database { get; set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }
}