using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface IVinculoHandler
{
    Task<Response<Vinculo?>> CriarAsync(CriarVinculoRequest request);
    Task<Response<Vinculo?>> AlterarAsync(AlterarVinculoRequest request);
    Task<Response<Vinculo?>> ExcluirAsync(ExcluirVinculoRequest request);
    Task<Response<Vinculo?>> ListarVinculoPorIdAsync(ListarVinculoPorIdRequest request);
    Task<PaginacaoResponse<List<Vinculo>?>> ListarVinculosPorNomeAsync(ListarVinculosPorNomeRequest request);
    Task<PaginacaoResponse<List<Vinculo>?>> ListarTodosVinculosAsync(ListarTodosVinculosRequest request);
}
