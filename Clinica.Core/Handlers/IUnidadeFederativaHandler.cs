using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadesFederativas;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface IUnidadeFederativaHandler
{
    Task<Response<UnidadeFederativa?>> CriarAsync(CriarUnidadeFederativaRequest request);
    Task<Response<UnidadeFederativa?>> AlterarAsync(AlterarUnidadeFederativaRequest request);
    Task<Response<UnidadeFederativa?>> ExcluirAsync(ExcluirUnidadeFederativaRequest request);
    Task<Response<UnidadeFederativa?>> ListarUnidadeFederativaPorIdAsync(ListarUnidadeFederativaPorIdRequest request);

    Task<PaginacaoResponse<List<UnidadeFederativa>?>> ListarTodasUnidadesFederativasAsync(ListarTodasUnidadesFederativasRequest request);
    Task<PaginacaoResponse<List<UnidadeFederativa>?>> ListarUnidadesFederativasPorNomeAsync(ListarUnidadesFederativasPorNomeRequest request);
    Task<PaginacaoResponse<List<UnidadeFederativa>?>> ListarUnidadesFederativasPorSiglaAsync(ListarUnidadesFederativasPorSiglaRequest request);
}
