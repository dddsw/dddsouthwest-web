using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using App.Metrics.Configuration;
using DDDSouthWest.Domain;
using DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.CreateNewEvent;
using DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.UpdateExistingEvent;
using DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.ViewEventDetail;
using DDDSouthWest.Domain.Features.Account.Admin.ManageNews.CreateNews;
using DDDSouthWest.Domain.Features.Account.Admin.ManageNews.UpdateExistingNews;
using DDDSouthWest.Domain.Features.Account.Admin.ManageNews.ViewNewsDetail;
using DDDSouthWest.Domain.Features.Account.Admin.ManagePages.CreatePage;
using DDDSouthWest.Domain.Features.Account.Admin.ManagePages.UpdateExistingPage;
using DDDSouthWest.Domain.Features.Account.Admin.ManageProfile.UpdateExistingProfile;
using DDDSouthWest.Domain.Features.Account.RegisterNewUser;
using DDDSouthWest.Domain.Features.Public.Page;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

            services.AddMediatR(typeof(UpsertSpeakerProfile.Command).GetTypeInfo().Assembly);

            services.AddWebsiteAppSettingsOptions(Configuration);
            
            // Validation            
            services.AddTransient<CreateNewEventValidator, CreateNewEventValidator>();
            services.AddTransient<UpdateExistingEventValidator, UpdateExistingEventValidator>();
                        
            services.AddTransient<UpsertSpeakerProfileValidator, UpsertSpeakerProfileValidator>();
            
            services.AddTransient<CreateNewsValidation, CreateNewsValidation>();
            services.AddTransient<UpdateExistingNewsValidator, UpdateExistingNewsValidator>();
            
            services.AddTransient<CreatePageValidation, CreatePageValidation>();
            services.AddTransient<UpdateExistingPageValidator, UpdateExistingPageValidator>();
            services.AddTransient<UpdateExistingPageValidator, UpdateExistingPageValidator>();
            
            services.AddTransient<RegisterNewUserValidator, RegisterNewUserValidator>();
            
            // Email Notification
            services.AddTransient<IRegistrationConfirmation, SendEmailConfirmation>();
            
            // Misc
            services.AddTransient<MarkdownTransformer, MarkdownTransformer>();
            services.AddTransient<QueryEventById, QueryEventById>();
            services.AddTransient<QueryAnyNewsById, QueryAnyNewsById>();
            services.AddTransient<CreateNewRegisteredUser, CreateNewRegisteredUser>();
            services.AddTransient<UpsertSpeakerProfileQuery, UpsertSpeakerProfileQuery>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AccessPolicies.OrganiserAccessPolicy,
                    policy => policy.RequireClaim("role", "organiser"));
                options.AddPolicy(AccessPolicies.RegisteredAccessPolicy,
                    policy => policy.RequireClaim("role", "registered"));
                options.AddPolicy(AccessPolicies.SpeakerAccessPolicy,
                    policy => policy.RequireClaim("role", "speaker"));
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
            
            /*app.UseMetrics();*/
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("page", "page/{*filename}", new {controller = "Page", action = "Index"});
            });
        }
    }
}