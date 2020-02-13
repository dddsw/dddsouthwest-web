using System.IO;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace DDDSouthWest.Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.File("/logs/log.txt", rollingInterval: RollingInterval.Hour, buffered: true)
                .WriteTo.Console()
                .CreateLogger();
            
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://*:5002")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}