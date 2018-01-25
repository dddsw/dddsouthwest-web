using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace DDDSouthWest.IdentityServer
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", new[] {"role"})
        };

        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
        {
            new ApiResource("api1", "My API")
        };

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(string websiteUrl) => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"api1"}
            },

            // resource owner password grant client
            new Client
            {
                ClientId = "ro.client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"api1"}
            },

            // OpenID Connect implicit flow client (MVC)
            new Client
            {
                ClientId = "mvc",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.Implicit,
                RequireConsent = false,
                RedirectUris = {$"{websiteUrl}/signin-oidc"},
/*                AllowOfflineAccess = true,*/
                PostLogoutRedirectUris = {$"{websiteUrl}/signout-callback-oidc"},
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "roles"
                }
            }
        };
    }
}