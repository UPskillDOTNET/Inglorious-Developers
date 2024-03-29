﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace UserAuthenticationServer.Clients
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
        {

             new Client
            {
                ClientId = "mvc",
                ClientName = "MVC Client",
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                ClientSecrets = new List<Secret> {new Secret("Secret".Sha256())},
              

                // where to redirect to after login
                RedirectUris = { "https://localhost:44312/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:44312/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "PrivAPI.read", "PrivAPI.write" , "PubAPI.read" , "PubAPI.write", "CAPI.read", "CAPI.write"
                },
                AllowOfflineAccess = true,
                RequirePkce=false
            },
              new Client
             {
                ClientId = "mobile.client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedCorsOrigins = {"http://localhost:19006"},

                ClientSecrets =
                {
                new Secret("Secret".Sha256())
                },
                 AllowedScopes = new List<string>
                {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "PrivAPI.read", "PrivAPI.write" , "PubAPI.read" , "PubAPI.write", "CAPI.read", "CAPI.write"
                },
                AllowOfflineAccess = true,
                RequirePkce=false
            }
        };
        }
    }
}
