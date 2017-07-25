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

            services.Configure<WebsiteConfigurationOptions>(configuration.GetSection("DDDSouthWestWebsite"));
            services.AddSingleton(p => p.GetRequiredService<IOptions<WebsiteConfigurationOptions>>().Value);
            services.AddSingleton(p => p.GetRequiredService<WebsiteConfigurationOptions>().IdentityServer);

            return services;
        }
    }
}