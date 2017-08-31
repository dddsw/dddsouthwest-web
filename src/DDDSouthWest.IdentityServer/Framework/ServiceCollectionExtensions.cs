using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DDDSouthWest.IdentityServer.Framework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthServerAppSettingsOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            /*services.Configure<ClientConfigurationOptions>(configuration.GetSection("DDDSouthWestWebsite"));*/
            
            services.Configure<AuthServerConfigurationOptions>(configuration.GetSection("DDDSouthWestIdentityServer"));
            services.AddSingleton(p => p.GetRequiredService<IOptions<AuthServerConfigurationOptions>>().Value);
            services.AddSingleton(p => p.GetRequiredService<Database>().ConnectionString);
            services.AddSingleton(p => p.GetRequiredService<Database>().Type);

            return services;
        }
    }
}