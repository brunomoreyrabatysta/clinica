using Clinica.Core.Models;
using Clinica.Core.Requests.Prontuarios;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface IProntuarioHandler
{
    Task<Response<Prontuario?>> CriarAsync(CriarProntuarioRequest request);
    Task<Response<Prontuario?>> AlterarAsync(AlterarProntuarioRequest request);
    Task<Response<Prontuario?>> ExcluirAsync(ExcluirProntuarioRequest request);
    Task<Response<Prontuario?>> ListarProntuarioPorIdAsync(ListarProntuarioPorIdRequest request);

    Task<PaginacaoResponse<List<Prontuario>?>> ListarTodosProntuariosAsync(ListarTodosProntuariosRequest request);
    Task<PaginacaoResponse<List<Prontuario>?>> ListarProntuariosPorContratoIdAsync(ListarProntuariosPorContratoIdRequest request);
}
