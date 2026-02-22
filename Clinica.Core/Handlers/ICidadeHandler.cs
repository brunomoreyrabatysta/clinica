using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface ICidadeHandler
{
    Task<Response<Cidade?>> CriarAsync(CriarCidadeRequest request);
    Task<Response<Cidade?>> AlterarAsync(AlterarCidadeRequest request);
    Task<Response<Cidade?>> ExcluirAsync(ExcluirCidadeRequest request);
    Task<Response<Cidade?>> ListarCidadePorIdAsync(ListarCidadePorIdRequest request);

    Task<PaginacaoResponse<List<Cidade>?>> ListarTodasCidadesAsync(ListarTodasCidadesRequest request);
    Task<PaginacaoResponse<List<Cidade>?>> ListarCidadesPorNomeAsync(ListarCidadesPorNomeRequest request);
    Task<PaginacaoResponse<List<Cidade>?>> ListarCidadesPorUnidadeFederativaAsync(ListarCidadesPorUnidadeFederativaRequest request);
}
