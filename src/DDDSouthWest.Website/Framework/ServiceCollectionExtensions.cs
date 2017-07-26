using DDDSouthWest.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DDDSouthWest.Website.Framework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebsiteAppSettingsOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ClientConfigurationOptions>(configuration.GetSection("DDDSouthWestWebsite"));

            services.AddSingleton(p => p.GetRequiredService<IOptions<AuthServerConfigurationOptions>>().Value);
            services.AddSingleton(p => p.GetRequiredService<AuthServerConfigurationOptions>().IdentityServer);
            
            services.AddSingleton(p => p.GetRequiredService<IOptions<ClientConfigurationOptions>>().Value);
            services.AddSingleton(p => p.GetRequiredService<ClientConfigurationOptions>().Database);

            return services;
        }
    }
}