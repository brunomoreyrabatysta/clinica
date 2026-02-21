using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Paciente;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class PacienteHandler(IHttpClientFactory httpClientFactory) : IPacienteHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Paciente?>> AlterarAsync(AlterarPacienteRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/pacientes/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Paciente?>>()
            ?? new Response<Paciente?>(null, 400, "Falha ao atualizar o paciente.");
    }

    public async Task<Response<Paciente?>> CriarAsync(CriarPacienteRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/pacientes", request);
        return await result.Content.ReadFromJsonAsync<Response<Paciente?>>()
            ?? new Response<Paciente?>(null, 400, "Falha ao criar o paciente.");
    }

    public async Task<Response<Paciente?>> ExcluirAsync(ExcluirPacienteRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/pacientes/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Paciente?>>()
            ?? new Response<Paciente?>(null, 400, "Falha ao excluir o paciente.");
    }

    public async Task<Response<Paciente?>> ListarPacientePorIdAsync(ListarPacientePorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<Paciente?>>($"v1/pacientes/{request.Id}")
            ?? new Response<Paciente?>(null, 400, "Não foi possível obter o paciente.");
    }

    public async Task<PaginacaoResponse<List<Paciente>?>> ListarTodosPacientesAsync(ListarTodosPacientesRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Paciente>?>>($"v1/pacientes")
            ?? new PaginacaoResponse<List<Paciente>?>(null, 400, "Não foi possível obter os pacientes.");
    }

    public async Task<PaginacaoResponse<List<Paciente>?>> ListarPacientesPorNomeAsync(ListarPacientesPorNomeRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Paciente>?>>($"v1/pacientes/{request.Nome}")
            ?? new PaginacaoResponse<List<Paciente>?>(null, 400, "Não foi possível obter os pacientes por nome.");
    }
}
