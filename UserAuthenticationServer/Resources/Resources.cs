using IdentityServer4.Models;
using System.Collections.Generic;

namespace UserAuthenticationServer.Resources
{
    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            },
            new IdentityResource{
                Name = "UserID",
                UserClaims = new []{"userID"}
                }
            };

        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
      
             new ApiResource
            {
                Name = "PrivateAPI",
                DisplayName = "Private Park Api",
                Description = "Allow the application to access the Private Park API on your behalf",
                Scopes = new List<string> { "PrivAPI.read", "PrivAPI.write"},
                ApiSecrets = new List<Secret> {new Secret("MoreScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            },
               new ApiResource
            {
                Name = "PublicAPI",
                DisplayName = "Public Park Api",
                Description = "Allow the application to access Public Park API on your behalf",
                Scopes = new List<string> { "PubAPI.read", "PubAPI.write"},
                ApiSecrets = new List<Secret> {new Secret("SoMeMoreScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            },
                  new ApiResource
            {
                Name = "CentralAPI",
                DisplayName = "Central Api",
                Description = "Allow the application to access Central API on your behalf",
                Scopes = new List<string> { "CAPI.read", "CAPI.write"},
                ApiSecrets = new List<Secret> {new Secret("EvenMoreScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            }

        };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
            new ApiScope("PrivAPI.read", "Read Access to Private Park API"),
            new ApiScope("PrivAPI.write", "Write Access to Private Park API"),
            new ApiScope("PubAPI.read", "Read Access to Public Park API"),
            new ApiScope("PubAPI.write", "Write Access to Public Park API"),
            new ApiScope("CAPI.read", "Read Access to Central API"),
            new ApiScope("CAPI.write", "Write Access to Central API"),

        };
        }
    }
}