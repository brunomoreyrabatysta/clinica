using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class TipoCursoHandler(IHttpClientFactory httpClientFactory) : ITipoCursoHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<TipoCurso?>> AlterarAsync(AlterarTipoCursoRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/tiposcurso/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<TipoCurso?>>()
            ?? new Response<TipoCurso?>(null, 400, "Falha ao atualizar o tipo de curso.");
    }

    public async Task<Response<TipoCurso?>> CriarAsync(CriarTipoCursoRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/tiposcurso", request);
        return await result.Content.ReadFromJsonAsync<Response<TipoCurso?>>() 
            ?? new Response<TipoCurso?>(null, 400, "Falha ao criar o tipo de curso.");
    }

    public async Task<Response<TipoCurso?>> ExcluirAsync(ExcluirTipoCursoRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/tiposcurso/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<TipoCurso?>>()
            ?? new Response<TipoCurso?>(null, 400, "Falha ao excluir o tipo de curso.");
    }

    public async Task<Response<TipoCurso?>> ListarPorIdAsync(ListarTipoCursoPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<TipoCurso?>>($"v1/tiposcurso/{request.Id}") 
            ?? new Response<TipoCurso?>(null, 400, "Não foi possível obter o tipo de curso.");
    }

    public async Task<PaginacaoResponse<List<TipoCurso>>> ListarTodosAsync(ListarTodosTipoCursoRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<TipoCurso>>>($"v1/tiposcurso")
            ?? new PaginacaoResponse<List<TipoCurso>>(null, 400, "Não foi possível obter os tipos de curso.");
    }
}
