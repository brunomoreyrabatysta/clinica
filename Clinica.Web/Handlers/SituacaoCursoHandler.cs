using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.SituacaoCurso;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class SituacaoCursoHandler(IHttpClientFactory httpClientFactory) : ISituacaoCursoHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<SituacaoCurso?>> AlterarAsync(AlterarSituacaoCursoRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/situacoescurso/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<SituacaoCurso?>>()
            ?? new Response<SituacaoCurso?>(null, 400, "Falha ao atualizar a situação de curso.");
    }

    public async Task<Response<SituacaoCurso?>> CriarAsync(CriarSituacaoCursoRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/situacoescurso", request);
        return await result.Content.ReadFromJsonAsync<Response<SituacaoCurso?>>()
            ?? new Response<SituacaoCurso?>(null, 400, "Falha ao criar a situação de curso.");
    }

    public async Task<Response<SituacaoCurso?>> ExcluirAsync(ExcluirSituacaoCursoRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/situacoescurso/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<SituacaoCurso?>>()
            ?? new Response<SituacaoCurso?>(null, 400, "Falha ao excluir a situação de curso.");
    }

    public async Task<Response<SituacaoCurso?>> ListarPorIdAsync(ListarSituacaoCursoPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<SituacaoCurso?>>($"v1/situacoescurso/{request.Id}")
            ?? new Response<SituacaoCurso?>(null, 400, "Não foi possível obter o tipo de curso.");
    }

    public async Task<PaginacaoResponse<List<SituacaoCurso>>> ListarTodasAsync(ListarTodasSituacaoCursoRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<SituacaoCurso>>>($"v1/situacoescurso")
            ?? new PaginacaoResponse<List<SituacaoCurso>>(null, 400, "Não foi possível obter as situações de curso.");
    }
}
