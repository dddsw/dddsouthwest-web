using System;
using System.Net.Http;
using DDDSouthWest.Website;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace DDDSouthWest.Tests.Framework
{
    public class HttpServerFixture : IDisposable
    {
        private bool _disposed;

        public HttpServerFixture() : this(null)
        {
        }

        protected HttpServerFixture(IWebHostBuilder builder)
        {
            if (builder == null)
            {
                builder = new WebHostBuilder()
                    .UseContentRoot(HostingHelpers.GetProjectPath<Startup>())
                    .UseEnvironment("Development") // Disable antiforgery checks and require-SSL
                    .UseStartup<TestStartup>();
            }

            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }

        ~HttpServerFixture()
        {
            Dispose(false);
        }

        public TestServer Server { get; }

        public HttpClient Client { get; }

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                Client?.Dispose();
                Server?.Dispose();
            }

            _disposed = true;
        }

        private sealed class TestStartup : Startup
        {
            public TestStartup(IHostingEnvironment env)
                : base(env)
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.test.json")
                    .AddEnvironmentVariables();

                Configuration = builder.Build();
            }
        }
    }
}