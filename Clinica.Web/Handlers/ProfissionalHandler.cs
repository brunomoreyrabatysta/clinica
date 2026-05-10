using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class ProfissionalHandler(IHttpClientFactory httpClientFactory) : IProfissionalHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Profissional?>> AlterarAsync(AlterarProfissionalRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/profissionais/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Profissional?>>()
            ?? new Response<Profissional?>(null, 400, "Falha ao atualizar o profissional.");
    }

    public async Task<Response<Profissional?>> CriarAsync(CriarProfissionalRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/profissionais", request);
        return await result.Content.ReadFromJsonAsync<Response<Profissional?>>()
            ?? new Response<Profissional?>(null, 400, "Falha ao criar o profissional.");
    }

    public async Task<Response<Profissional?>> ExcluirAsync(ExcluirProfissionalRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/profissionais/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Profissional?>>()
            ?? new Response<Profissional?>(null, 400, "Falha ao excluir o profissional.");
    }

    public async Task<Response<Profissional?>> ListarProfissionalPorIdAsync(ListarProfissionalPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<Profissional?>>($"v1/profissionais/{request.Id}")
            ?? new Response<Profissional?>(null, 400, "Não foi possível obter o profissional.");
    }

    public async Task<PaginacaoResponse<List<Profissional>?>> ListarTodosProfissionaisAsync(ListarTodosProfissionaisRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Profissional>?>>($"v1/profissionais")
            ?? new PaginacaoResponse<List<Profissional>?>(null, 400, "Não foi possível obter os profissionais.");
    }

    public async Task<PaginacaoResponse<List<Profissional>?>> ListarProfissionaisPorNomeAsync(ListarProfissionaisPorNomeRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Profissional>?>>($"v1/profissionais/{request.Nome}")
            ?? new PaginacaoResponse<List<Profissional>?>(null, 400, "Não foi possível obter os profissionais por nome.");
    }
}