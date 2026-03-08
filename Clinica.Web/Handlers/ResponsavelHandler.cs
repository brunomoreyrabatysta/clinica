using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class ResponsavelHandler(IHttpClientFactory httpClientFactory) : IResponsavelHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Responsavel?>> AlterarAsync(AlterarResponsavelRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/responsaveis/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Responsavel?>>()
            ?? new Response<Responsavel?>(null, 400, "Falha ao atualizar o responsável.");
    }

    public async Task<Response<Responsavel?>> CriarAsync(CriarResponsavelRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/responsaveis", request);
        return await result.Content.ReadFromJsonAsync<Response<Responsavel?>>()
            ?? new Response<Responsavel?>(null, 400, "Falha ao criar o responsável.");
    }

    public async Task<Response<Responsavel?>> ExcluirAsync(ExcluirResponsavelRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/responsaveis/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Responsavel?>>()
            ?? new Response<Responsavel?>(null, 400, "Falha ao excluir o responsável.");
    }

    public async Task<Response<Responsavel?>> ListarResponsavelPorIdAsync(ListarResponsavelPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<Responsavel?>>($"v1/responsaveis/{request.Id}")
            ?? new Response<Responsavel?>(null, 400, "Não foi possível obter o responsável.");
    }

    public async Task<PaginacaoResponse<List<Responsavel>?>> ListarTodosResponsaveisAsync(ListarTodosResponsaveisRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Responsavel>?>>($"v1/responsaveis")
            ?? new PaginacaoResponse<List<Responsavel>?>(null, 400, "Não foi possível obter os responsável.");
    }

    public async Task<PaginacaoResponse<List<Responsavel>?>> ListarResponsaveisPorNomeAsync(ListarResponsaveisPorNomeRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Responsavel>?>>($"v1/responsaveis/{request.Nome}")
            ?? new PaginacaoResponse<List<Responsavel>?>(null, 400, "Não foi possível obter os responsáveis por nome.");
    }
}
