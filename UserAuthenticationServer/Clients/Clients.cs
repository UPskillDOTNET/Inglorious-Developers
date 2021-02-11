using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                ClientId = "oauthClient",
                ClientName = "Example client application using client credentials",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {new Secret("ThIsIsAnotHERsuPerSecretPassWord".Sha256())}, // change me!
                AllowedScopes = new List<string> {"PrivAPI.read", "PrivAPI.write" , "PubAPI.read" , "PubAPI.write", "CAPI.read", "CAPI.write" }
            }
        };
        }
    }
}
