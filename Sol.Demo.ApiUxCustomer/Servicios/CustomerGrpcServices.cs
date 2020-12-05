using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Core;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Sol.Demo.Banca.Protos;
using Sol.Demo.ApiUxCustomer.Helpers;
using Sol.Demo.Comunes.DTO;
using Sol.Demo.Comunes.BE;


namespace Sol.Demo.ApiUxCustomer.Servicios
{    
    public class CustomerGrpcServices : ICustomerServices
    {
        private readonly ITokenAdapter tokenAdapter;
        private readonly IConfiguration configuration;

        public CustomerGrpcServices(
            ITokenAdapter _tokenAdapter, IConfiguration _configuration
        )
        {
            this.tokenAdapter = _tokenAdapter;
            this.configuration = _configuration;
        }

        public async Task<SaldoCuentaResponseBE> ConsultaSaldo(int codCuenta)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var miHttpClient = new HttpClient(handler);

            TokenResponseDTO responseToken = await tokenAdapter.GeneraToken();

            var metadata = new Metadata();
            metadata.Add("Authorization", $"Bearer {responseToken.Token}");

            var channel = GrpcChannel.ForAddress
                (configuration.GetValue<string>("UrlApiCliente"), 
                new GrpcChannelOptions { HttpClient = miHttpClient});

            BancaServiceGrpc.BancaServiceGrpcClient client =
                new BancaServiceGrpc.BancaServiceGrpcClient(channel);

            CuentaRequest request = new CuentaRequest()
            {
                IdCuenta= codCuenta
            };
            CuentaResponse response = await client.ConsultarSaldoCtaAsync
                (request, headers: metadata);

            SaldoCuentaResponseBE txResponseBE = new SaldoCuentaResponseBE() {
                Amount = (decimal)response.Saldo,
                Date = DateTime.Now
            };

            return txResponseBE;
            //return null;
        }

        public Task<CustomerBE> RecuperarPorDNI(string nroDocumento)
        {
            throw new System.NotImplementedException();
        }
    }
}