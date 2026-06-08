using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Prontuarios;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class ProntuarioHandler(IHttpClientFactory httpClientFactory) : IProntuarioHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Prontuario?>> AlterarAsync(AlterarProntuarioRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/prontuarios/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Prontuario?>>()
            ?? new Response<Prontuario?>(null, 400, "Falha ao atualizar o prontuário.");
    }

    public async Task<Response<Prontuario?>> CriarAsync(CriarProntuarioRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/prontuarios", request);
        return await result.Content.ReadFromJsonAsync<Response<Prontuario?>>()
            ?? new Response<Prontuario?>(null, 400, "Falha ao criar o prontuário.");
    }

    public async Task<Response<Prontuario?>> ExcluirAsync(ExcluirProntuarioRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/prontuarios/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Prontuario?>>()
            ?? new Response<Prontuario?>(null, 400, "Falha ao excluir o prontuário.");
    }

    public async Task<Response<Prontuario?>> ListarProntuarioPorIdAsync(ListarProntuarioPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<Prontuario?>>($"v1/prontuarios/{request.Id}")
            ?? new Response<Prontuario?>(null, 400, "Não foi possível obter o prontuário.");
    }

    public async Task<PaginacaoResponse<List<Prontuario>?>> ListarProntuariosPorContratoIdAsync(ListarProntuariosPorContratoIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Prontuario>?>>($"v1/prontuarios/contrato/{request.ContratoId}")
            ?? new PaginacaoResponse<List<Prontuario>?>(null, 400, "Não foi possível obter os prontuários por contrato.");
    }

    public async Task<PaginacaoResponse<List<Prontuario>?>> ListarTodosProntuariosAsync(ListarTodosProntuariosRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Prontuario>?>>($"v1/prontuarios")
            ?? new PaginacaoResponse<List<Prontuario>?>(null, 400, "Não foi possível obter os prontuários.");
    }
}
