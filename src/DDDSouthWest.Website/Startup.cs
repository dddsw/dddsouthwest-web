using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent;
using DDDSouthWest.Domain.Features.Account.ManageEvents.GetEvent;
using DDDSouthWest.Domain.Features.Account.ManageEvents.UpdateExistingEvent;
using DDDSouthWest.Domain.Features.Account.ManageEvents.ViewEventDetail;
using DDDSouthWest.Domain.Features.Account.ManageNews.CreateNews;
using DDDSouthWest.Domain.Features.Account.ManageNews.UpdateExistingNews;
using DDDSouthWest.Domain.Features.Account.ManageNews.ViewNewsDetail;
using DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage;
using DDDSouthWest.Domain.Features.Account.RegisterNewUser;
using DDDSouthWest.Domain.Features.Public.Page;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddMvc().AddFeatureFolders();

            services.AddMediatR(typeof(GetPage.Query).GetTypeInfo().Assembly);

            services.AddWebsiteAppSettingsOptions(Configuration);
            
            // Validation
            services.AddTransient<CreateNewEventValidator, CreateNewEventValidator>();
            services.AddTransient<UpdateExistingEventValidator, UpdateExistingEventValidator>();
                        
            services.AddTransient<CreateNewsValidation, CreateNewsValidation>();
            services.AddTransient<UpdateExistingNewsValidator, UpdateExistingNewsValidator>();
            
            services.AddTransient<CreatePageValidation, CreatePageValidation>();
            services.AddTransient<RegisterNewUserValidator, RegisterNewUserValidator>();
            
            services.AddTransient<QueryEventById, QueryEventById>();
            services.AddTransient<QueryAnyNewsById, QueryAnyNewsById>();
            services.AddTransient<CreateNewRegisteredUser, CreateNewRegisteredUser>();
            
            // Email Notification
            services.AddTransient<IRegistrationConfirmation, SendEmailConfirmation>();

            services.AddMetrics()
                .AddHealthChecks()
                .AddJsonSerialization()
                .AddMetricsMiddleware(options => options.IgnoredHttpStatusCodes = new [] {404});
            
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ClientConfigurationOptions configurationOptions)
        {
            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

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
            
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("page", "page/{*filename}", new {controller = "Page", action = "Index"});
            });
        }
    }
}