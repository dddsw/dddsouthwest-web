using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using DDDSouthWest.Domain.Features.Account.ManageEvents.CreateEvent;
using DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage;
using DDDSouthWest.Domain.Features.Public.Page;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDSouthWest.Website
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddMvc().AddFeatureFolders();
            services.AddMediatR(typeof(GetPage.Query).GetTypeInfo().Assembly);

            services.AddWebsiteAppSettingsOptions(Configuration);
            
            services.AddTransient<CreateEventValidation, CreateEventValidation>();
            services.AddTransient<CreatePageValidation, CreatePageValidation>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AccessPolicies.OrganiserAccessPolicy,
                    policy => policy.RequireClaim("role", "organiser"));
                options.AddPolicy(AccessPolicies.RegisteredAccessPolicy,
                    policy => policy.RequireClaim("role", "registered"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, WebsiteConfigurationOptions configurationOptions)
        {
            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                AuthenticationScheme = "oidc",
                SignInScheme = "Cookies",
                Authority = configurationOptions.IdentityServer.AuthorityServer,
                RequireHttpsMetadata = false,
                ClientId = "mvc",
                SaveTokens = true,
                Scope = {"roles"},
                GetClaimsFromUserInfoEndpoint = true
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("page", "page/{*filename}", new {controller = "Page", action = "Index"});
            });
        }
    }
}