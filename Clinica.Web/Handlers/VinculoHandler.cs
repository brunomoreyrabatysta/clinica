using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class VinculoHandler(IHttpClientFactory httpClientFactory) : IVinculoHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Vinculo?>> AlterarAsync(AlterarVinculoRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/vinculos/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Vinculo?>>()
            ?? new Response<Vinculo?>(null, 400, "Falha ao atualizar o vínculo.");
    }

    public async Task<Response<Vinculo?>> CriarAsync(CriarVinculoRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/vinculos", request);
        return await result.Content.ReadFromJsonAsync<Response<Vinculo?>>()
            ?? new Response<Vinculo?>(null, 400, "Falha ao criar o vínculo.");
    }

    public async Task<Response<Vinculo?>> ExcluirAsync(ExcluirVinculoRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/vinculos/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Vinculo?>>()
            ?? new Response<Vinculo?>(null, 400, "Falha ao excluir o vínculo.");
    }

    public async Task<Response<Vinculo?>> ListarVinculoPorIdAsync(ListarVinculoPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<Vinculo?>>($"v1/vinculos/{request.Id}")
            ?? new Response<Vinculo?>(null, 400, "Não foi possível obter o vínculo.");
    }

    public async Task<PaginacaoResponse<List<Vinculo>?>> ListarTodosVinculosAsync(ListarTodosVinculosRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Vinculo>?>>($"v1/vinculos")
            ?? new PaginacaoResponse<List<Vinculo>?>(null, 400, "Não foi possível obter os vínculos.");
    }

    public async Task<PaginacaoResponse<List<Vinculo>?>> ListarVinculosPorNomeAsync(ListarVinculosPorNomeRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Vinculo>?>>($"v1/vinculos/{request.Nome}")
            ?? new PaginacaoResponse<List<Vinculo>?>(null, 400, "Não foi possível obter os vínculos.");
    }
}
