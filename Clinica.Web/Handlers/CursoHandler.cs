using Clinica.Core.Common.Extensions;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Curso;
using Clinica.Core.Responses;
using System.Net.Http;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class CursoHandler(IHttpClientFactory httpClientFactory) : ICursoHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Curso?>> AlterarAsync(AlterarCursoRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/cursos/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Curso?>>()
            ?? new Response<Curso?>(null, 400, "Falha ao atualizar o curso.");
    }

    public async Task<Response<Curso?>> CriarAsync(CriarCursoRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/cursos", request);
        return await result.Content.ReadFromJsonAsync<Response<Curso?>>()
            ?? new Response<Curso?>(null, 400, "Falha ao criar o curso.");
    }

    public async Task<Response<Curso?>> ExcluirAsync(ExcluirCursoRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/cursos/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Curso?>>()
            ?? new Response<Curso?>(null, 400, "Falha ao excluir o curso.");
    }

    public async Task<Response<Curso?>> ListarCursoPorIdAsync(ListarCursoPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<Curso?>>($"v1/cursos/{request.Id}")
            ?? new Response<Curso?>(null, 400, "Não foi possível obter o curso.");
    }

    public async Task<PaginacaoResponse<List<Curso>?>> ListarCursoPorPeriodoAsync(ListarCursoPorPeriodoRequest request)
    {
        const string format = "yyyy-MM-dd";
        string dataInicio = request.DataInicio is not null 
            ? request.DataInicio.Value.ToString(format)
            : DateTime.Now.ObterPrimeiroDia().ToString(format);

        string dataTermino = request.DataTermino is not null
            ? request.DataTermino.Value.ToString(format)
            : DateTime.Now.ObterUltimoDia().ToString(format);

        var url = $"v1/cursos?dataInicio={dataInicio}&dataTermino={dataTermino}";

        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Curso>?>>(url)
            ?? new PaginacaoResponse<List<Curso>?>(null, 400, "Não foi possível os cursos.");        
    }

    public async Task<PaginacaoResponse<List<Curso>?>> ListarCursoPorTipoCursoAsync(ListarCursoPorTipoCursoRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginacaoResponse<List<Curso>?>> ListarTodosCursoAsync(ListarTodosCursoRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Curso>?>>($"v1/cursos")
            ?? new PaginacaoResponse<List<Curso>?>(null, 400, "Não foi possível obter os cursos.");
    }
}
