using Clinica.Core.Models;
using Clinica.Core.Requests.Curso;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface ICursoHandler
{
    Task<Response<Curso?>> CriarAsync(CriarCursoRequest request);
    Task<Response<Curso?>> AlterarAsync(AlterarCursoRequest request);
    Task<Response<Curso?>> ExcluirAsync(ExcluirCursoRequest request);
    Task<Response<Curso?>> ListarCursoPorIdAsync(ListarCursoPorIdRequest request);

    Task<PaginacaoResponse<List<Curso>?>> ListarTodosCursoAsync(ListarTodosCursoRequest request);
    Task<PaginacaoResponse<List<Curso>?>> ListarCursoPorPeriodoAsync(ListarCursoPorPeriodoRequest request);
    Task<PaginacaoResponse<List<Curso>?>> ListarCursoPorTipoCursoAsync(ListarCursoPorTipoCursoRequest request);
}
