using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Reflection;
using DDDSouthWest.Domain.Features.Public.Page;
using DDDSouthWest.Website.Framework;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DDDSouthWest.Website
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
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

            /*services.AddAuthorization(options =>
                options.AddPolicy(AccessPolicies.OrganiserAccessPolicy, policy => policy.RequireClaim("role", "organiser")));*/

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AccessPolicies.OrganiserAccessPolicy, policy => policy.RequireClaim("role"));
            });
            
            /*services.AddAuthorization(options => options.AddPolicy("IsValidUser", ResolvePolicy));*/
            
            
                
        }
        
        private void ResolvePolicy(AuthorizationPolicyBuilder policy)
        {
            policy.Requirements.Add(new UsernameRequirement("administrator"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,
                ClientId = "mvc",
                SaveTokens = true,
                Scope = { "roles" },
                GetClaimsFromUserInfoEndpoint = true
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
           
            // Adds IdentityServer
            // app.UseIdentityServer();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "page", template: "page/{*filename}", defaults: new { controller = "Page", action = "Index" });
            });
        }
    }
}
