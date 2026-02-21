using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface ITipoCursoHandler
{
    Task<Response<TipoCurso?>> CriarAsync(CriarTipoCursoRequest request);
    Task<Response<TipoCurso?>> AlterarAsync(AlterarTipoCursoRequest request);
    Task<Response<TipoCurso?>> ExcluirAsync(ExcluirTipoCursoRequest request);
    Task<Response<TipoCurso?>> ListarPorIdAsync(ListarTipoCursoPorIdRequest request);
    Task<PaginacaoResponse<List<TipoCurso>>> ListarTodosAsync(ListarTodosTipoCursoRequest request);
}
