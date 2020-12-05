using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Sol.Demo.Comunes.Configs;
using Sol.Demo.Comunes.DTO;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sol.Demo.ApiUxCustomer.Helpers
{
    public class TokenAdapter : ITokenAdapter
    {
        private readonly IdentityServerConfig options;

        public TokenAdapter(IOptions<IdentityServerConfig> options)
        {
            this.options = options.Value;
        }

        public async Task<TokenResponseDTO> GeneraToken()
        {
            TokenResponseDTO tokenResponseDTO =
                new TokenResponseDTO { Process = false };

            var client = new HttpClient();
            //var disco = await client.GetDiscoveryDocumentAsync(options.UrlServer);

            var disco = await client.GetDiscoveryDocumentAsync(
             new DiscoveryDocumentRequest
             {
                 Address = options.UrlServer,
                 Policy = {
                        RequireHttps = false
                 }
             });

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return tokenResponseDTO;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,

                    ClientId = options.ClienteId,
                    ClientSecret = options.ClienteSecret,
                    Scope = options.ResourceAccess
                });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return tokenResponseDTO;
            }
            tokenResponseDTO.Process = true;
            tokenResponseDTO.Token = tokenResponse.AccessToken;

            return tokenResponseDTO;
        }
    }
}
