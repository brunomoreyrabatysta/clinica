using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadeFederativa;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class UnidadeFederativaHandler(IHttpClientFactory httpClientFactory) : IUnidadeFederativaHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<UnidadeFederativa?>> AlterarAsync(AlterarUnidadeFederativaRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/unidadesfederativas/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<UnidadeFederativa?>>()
            ?? new Response<UnidadeFederativa?>(null, 400, "Falha ao atualizar a unidade federativa.");
    }

    public async Task<Response<UnidadeFederativa?>> CriarAsync(CriarUnidadeFederativaRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/unidadesfederativas", request);
        return await result.Content.ReadFromJsonAsync<Response<UnidadeFederativa?>>()
            ?? new Response<UnidadeFederativa?>(null, 400, "Falha ao criar a unidade federativa.");
    }

    public async Task<Response<UnidadeFederativa?>> ExcluirAsync(ExcluirUnidadeFederativaRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/unidadesfederativas/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<UnidadeFederativa?>>()
            ?? new Response<UnidadeFederativa?>(null, 400, "Falha ao excluir a unidade federativa.");
    }

    public async Task<Response<UnidadeFederativa?>> ListarUnidadeFederativaPorIdAsync(ListarUnidadeFederativaPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<UnidadeFederativa?>>($"v1/unidadesfederativas/{request.Id}")
            ?? new Response<UnidadeFederativa?>(null, 400, "Não foi possível obter a unidade federativa.");
    }

    public async Task<PaginacaoResponse<List<UnidadeFederativa>?>> ListarTodasUnidadesFederativasAsync(ListarTodasUnidadesFederativasRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<UnidadeFederativa>?>>($"v1/unidadesfederativas")
            ?? new PaginacaoResponse<List<UnidadeFederativa>?>(null, 400, "Não foi possível obter as unidades federativas.");
    }

    public async Task<PaginacaoResponse<List<UnidadeFederativa>?>> ListarUnidadesFederativasPorNomeAsync(ListarUnidadesFederativasPorNomeRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<UnidadeFederativa>?>>($"v1/unidadesfederativas/nome/{request.Nome}")
            ?? new PaginacaoResponse<List<UnidadeFederativa>?>(null, 400, "Não foi possível obter as unidades federativas por nome.");
    }

    public async Task<PaginacaoResponse<List<UnidadeFederativa>?>> ListarUnidadesFederativasPorSiglaAsync(ListarUnidadesFederativasPorSiglaRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<UnidadeFederativa>?>>($"v1/unidadesfederativas/sigla/{request.Sigla}")
            ?? new PaginacaoResponse<List<UnidadeFederativa>?>(null, 400, "Não foi possível obter as unidades federativas por sigla.");
    }
}
