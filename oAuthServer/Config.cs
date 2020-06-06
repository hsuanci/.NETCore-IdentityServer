// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using System.Collections.Generic;

namespace auth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // Client Credentials Grant Type Flow
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // Authorization Code Grant Type Flow
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequireClientSecret = true,
                    ClientSecrets = { new Secret("code".Sha256()) },

                    RedirectUris =
                    {
                        "http://localhost:4200/login",
                    },
                    AccessTokenLifetime = 20, // Token life limit
                    //AbsoluteRefreshTokenLifetime = 10, // Refresh token life limit
                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1", "offline_access" }
                },

                // Implicit Grant Type Flow
                new Client
                {
                    ClientId = "spaImplicit",
                    ClientName = "SPA Implicit",
                    ClientUri = "http://identityserver.io",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireClientSecret = true,
                    ClientSecrets = new [] { new Secret("implicit".Sha256()) },

                    RedirectUris =
                    {
                        "http://localhost:5002/callback.html",
                    },
                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowedScopes = { "openid", "profile", "api1"},
                    AllowAccessTokensViaBrowser = true
                }, 

                // Resource Owner Password Credentials Grant Type Flow
                new Client
                {
                    ClientId = "spaResource",
                    ClientName = "SPA Resource",
                    ClientUri = "http://identityserver.io",
                    ClientSecrets = new [] { new Secret("resource".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    RedirectUris =
                    {
                        "http://localhost:5002/callback.html",
                    },
                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowedScopes = { "openid", "profile", "api1"},
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}