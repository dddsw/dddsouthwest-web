using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
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
using DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.UpdateExistingTalk;
using DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.AddNewTalk;
using DDDSouthWest.Domain.Features.Account.RegisterNewUser;
using DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.UpdateExistingTalk;
using DDDSouthWest.Domain.Features.Public.Volunteer;
using DDDSouthWest.Website.Framework;
using DDDSouthWest.Website.ImageHandlers;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

[assembly: AspMvcViewLocationFormat(@"~\Features\Public\{1}\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Admin\{0}.cshtml")]
[assembly: AspMvcViewLocationFormat(@"~\Features\Shared\{0}.cshtml")]
namespace DDDSouthWest.Website
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
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

            var configurationOptions = Configuration.GetSection("DDDSouthWestWebsite").Get<ClientConfigurationOptions>();
            
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    options.Authority = configurationOptions.IdentityServer.AuthorityServer;
                    options.ClientId = "mvc";
                    options.RequireHttpsMetadata = false;
                    options.SaveTokens = true;
                    options.SignInScheme = "Cookies";
                    options.Scope.Add("roles");
                    options.GetClaimsFromUserInfoEndpoint = true;
                });

            services.AddControllers().AddFeatureFolders();

            services.AddMediatR(typeof(UpsertSpeakerProfile.Command).GetTypeInfo().Assembly);

            services.AddWebsiteAppSettingsOptions(Configuration);
            
            // Validation            
            services.AddTransient<CreateNewEventValidator, CreateNewEventValidator>();
            services.AddTransient<UpdateExistingEventValidator, UpdateExistingEventValidator>();
            services.AddTransient<AddNewTalkValidator, AddNewTalkValidator>();
            services.AddTransient<UpdateExistingTalkAsAdminValidator, UpdateExistingTalkAsAdminValidator>();
                        
            services.AddTransient<UpsertSpeakerProfileValidator, UpsertSpeakerProfileValidator>();
            
            services.AddTransient<CreateNewsValidation, CreateNewsValidation>();
            services.AddTransient<UpdateExistingNewsValidator, UpdateExistingNewsValidator>();
            services.AddTransient<UpdateExistingTalkValidator, UpdateExistingTalkValidator>();

            services.AddTransient<CreatePageValidation, CreatePageValidation>();
            services.AddTransient<UpdateExistingPageValidator, UpdateExistingPageValidator>();

            services.AddTransient<VolunteerRegistrationValidation, VolunteerRegistrationValidation>();
            
            services.AddTransient<RegisterNewUserValidator, RegisterNewUserValidator>();
            
            // Email Notification
            services.AddTransient<IRegistrationConfirmation, SendEmailConfirmation>();
            
            // Misc
            services.AddTransient<MarkdownTransformer, MarkdownTransformer>();
            services.AddTransient<QueryEventById, QueryEventById>();
            services.AddTransient<QueryAnyNewsById, QueryAnyNewsById>();
            services.AddTransient<CreateNewRegisteredUser, CreateNewRegisteredUser>();
            services.AddTransient<UpsertSpeakerProfileQuery, UpsertSpeakerProfileQuery>();
            services.AddTransient<ProfileImageHandler, ProfileImageHandler>();

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerfactory,
            IHostApplicationLifetime appLifetime)
        {
            loggerfactory.AddSerilog();
            
            var forwardedHeadersOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            forwardedHeadersOptions.KnownProxies.Clear();
            forwardedHeadersOptions.KnownNetworks.Clear();

            app.UseForwardedHeaders(forwardedHeadersOptions);

            app.UseStaticFiles();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseAuthentication();
            
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            appLifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }
}