// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Security.Claims;
using DDDSouthWest.IdentityServer.Framework;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

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
        public static IEnumerable<Client> GetClients() => new List<Client>
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
                /*RequireConsent = false,*/
                RedirectUris = {"http://localhost:5002/signin-oidc"},
                PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles",
                    IdentityServerConstants.StandardScopes.Email
                }
            }
        };

        public static List<TestUser> GetUsers() => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "josephwoodward",
                Password = "letmein",
                Claims = new List<Claim>
                {
                    new Claim("name", "Joseph"),
                    new Claim("website", "https://alice.com"),
                    new Claim("role", "organiser")
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "bob",
                Password = "password",

                Claims = new List<Claim>
                {
                    new Claim("name", "Bob"),
                    new Claim("website", "https://bob.com"),
                    new Claim("role", Role.Speaker)
                }
            }
        };
    }
}