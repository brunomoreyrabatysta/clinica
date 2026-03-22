using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class ContratoHandler(IHttpClientFactory httpClientFactory) : IContratoHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Contrato?>> AlterarAsync(AlterarContratoRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/contratos/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Contrato?>>()
            ?? new Response<Contrato?>(null, 400, "Falha ao atualizar o contrato.");
    }

    public async Task<Response<Contrato?>> CriarAsync(CriarContratoRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/contratos", request);
        return await result.Content.ReadFromJsonAsync<Response<Contrato?>>()
            ?? new Response<Contrato?>(null, 400, "Falha ao criar o contrato.");
    }

    public async Task<Response<Contrato?>> ExcluirAsync(ExcluirContratoRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/contratos/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Contrato?>>()
            ?? new Response<Contrato?>(null, 400, "Falha ao excluir o contrato.");
    }

    public async Task<Response<Contrato?>> ListarContratoPorIdAsync(ListarContratoPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<Contrato?>>($"v1/contratos/{request.Id}")
            ?? new Response<Contrato?>(null, 400, "Não foi possível obter o contrato.");
    }

    public async Task<PaginacaoResponse<List<Contrato>?>> ListarTodosContratosAsync(ListarTodosContratosRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Contrato>?>>($"v1/contratos")
            ?? new PaginacaoResponse<List<Contrato>?>(null, 400, "Não foi possível obter os contratos.");
    }
}
