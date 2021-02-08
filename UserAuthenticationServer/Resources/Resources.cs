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
                Description = "Allow the application to access API #1 on your behalf",
                Scopes = new List<string> { "PrivAPI.read", "PrivAPI.write"},
                ApiSecrets = new List<Secret> {new Secret("SoMeMoreScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            },
      
        };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
            new ApiScope("PrivAPI.read", "Read Access to Private Park API"),
            new ApiScope("PrivAPI.write", "Write Access to Private Park API"),
        };
        }
    }
}