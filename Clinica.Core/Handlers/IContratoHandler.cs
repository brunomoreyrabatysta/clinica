using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface IContratoHandler
{
    Task<Response<Contrato?>> CriarAsync(CriarContratoRequest request);
    Task<Response<Contrato?>> AlterarAsync(AlterarContratoRequest request);
    Task<Response<Contrato?>> ExcluirAsync(ExcluirContratoRequest request);
    Task<Response<Contrato?>> ListarContratoPorIdAsync(ListarContratoPorIdRequest request);
    Task<PaginacaoResponse<List<Contrato>?>> ListarTodosContratosAsync(ListarTodosContratosRequest request);
}
