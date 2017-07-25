using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DDDSouthWest.IdentityServer.Framework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebsiteAppSettingsOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<IdentityServer>(configuration.GetSection("DDDSouthWestIdentityServer"));
            services.AddSingleton(p => p.GetRequiredService<IOptions<IdentityServer>>().Value);
            services.AddSingleton(p => p.GetRequiredService<IdentityServer>().WebsiteUrl);

            return services;
        }
    }
}