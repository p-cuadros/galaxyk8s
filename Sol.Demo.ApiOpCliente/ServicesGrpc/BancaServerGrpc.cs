using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Sol.Demo.ApiOpCliente.Services;
using Sol.Demo.Banca.Protos;
using Sol.Demo.Comunes.BE;
namespace Sol.Demo.ApiOpCliente.ServicesGrpc
{
    [Authorize]
    public class BancaServerGrpc : BancaServiceGrpc.BancaServiceGrpcBase
    {
        private readonly ICuentaServices cuentaServices;
        private readonly ILogger<BancaServerGrpc> logger;
        public BancaServerGrpc(
            ICuentaServices _cuentaServices,
            ILogger<BancaServerGrpc> _logger)
        {
            this.cuentaServices = _cuentaServices;
            this.logger = _logger;
        }

        public override async Task<CuentaResponse> ConsultarSaldoCta
            (CuentaRequest request, ServerCallContext _context)
        {
            logger.LogWarning("Llego a opbanca grpc");

            // TxRequestBE be = new TxRequestBE()
            // {
            //     IdCuentaDestino = request.IdCuentaDestino,
            // };
            SaldoCuentaResponseBE resp = await cuentaServices.RecuperarSaldoIdCuenta(request.IdCuenta);
            CuentaResponse txRpta = new CuentaResponse()
            {
                Saldo = (double)resp.Amount
            };
            return txRpta;
        }
    }
}