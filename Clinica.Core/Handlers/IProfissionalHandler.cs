using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface IProfissionalHandler
{
    Task<Response<Profissional?>> CriarAsync(CriarProfissionalRequest request);
    Task<Response<Profissional?>> AlterarAsync(AlterarProfissionalRequest request);
    Task<Response<Profissional?>> ExcluirAsync(ExcluirProfissionalRequest request);
    Task<Response<Profissional?>> ListarProfissionalPorIdAsync(ListarProfissionalPorIdRequest request);
    Task<PaginacaoResponse<List<Profissional>?>> ListarProfissionaisPorNomeAsync(ListarProfissionaisPorNomeRequest request);
    Task<PaginacaoResponse<List<Profissional>?>> ListarTodosProfissionaisAsync(ListarTodosProfissionaisRequest request);
}
