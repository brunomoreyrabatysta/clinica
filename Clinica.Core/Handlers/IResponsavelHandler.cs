using Clinica.Core.Models;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface IResponsavelHandler
{
    Task<Response<Responsavel?>> CriarAsync(CriarResponsavelRequest request);
    Task<Response<Responsavel?>> AlterarAsync(AlterarResponsavelRequest request);
    Task<Response<Responsavel?>> ExcluirAsync(ExcluirResponsavelRequest request);
    Task<Response<Responsavel?>> ListarResponsavelPorIdAsync(ListarResponsavelPorIdRequest request);
    Task<PaginacaoResponse<List<Responsavel>?>> ListarResponsaveisPorNomeAsync(ListarResponsaveisPorNomeRequest request);
    Task<PaginacaoResponse<List<Responsavel>?>> ListarTodosResponsaveisAsync(ListarTodosResponsaveisRequest request);
}
