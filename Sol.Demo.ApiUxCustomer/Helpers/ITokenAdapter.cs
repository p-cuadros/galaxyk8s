using Sol.Demo.Comunes.DTO;
using System.Threading.Tasks;

namespace Sol.Demo.ApiUxCustomer.Helpers
{
    public interface ITokenAdapter
    {
        Task<TokenResponseDTO> GeneraToken();
    }
}
