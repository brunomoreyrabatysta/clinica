using Clinica.Core.Models;
using Clinica.Core.Requests.SituacaoCurso;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface ISituacaoCursoHandler
{
    Task<Response<SituacaoCurso?>> CriarAsync(CriarSituacaoCursoRequest request);
    Task<Response<SituacaoCurso?>> AlterarAsync(AlterarSituacaoCursoRequest request);
    Task<Response<SituacaoCurso?>> ExcluirAsync(ExcluirSituacaoCursoRequest request);
    Task<Response<SituacaoCurso?>> ListarPorIdAsync(ListarSituacaoCursoPorIdRequest request);
    Task<PaginacaoResponse<List<SituacaoCurso>>> ListarTodasAsync(ListarTodasSituacaoCursoRequest request);
}
