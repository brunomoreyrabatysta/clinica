using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers;

public class FinanceiroHandler(IHttpClientFactory httpClientFactory) : IFinanceiroHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Financeiro?>> AlterarAsync(AlterarFinanceiroRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/financeiros/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Financeiro?>>()
            ?? new Response<Financeiro?>(null, 400, "Falha ao atualizar o financeiro.");
    }

    public async Task<Response<Financeiro?>> CriarAsync(CriarFinanceiroRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/financeiros", request);
        return await result.Content.ReadFromJsonAsync<Response<Financeiro?>>()
            ?? new Response<Financeiro?>(null, 400, "Falha ao criar o financeiro.");
    }

    public async Task<Response<Financeiro?>> ExcluirAsync(ExcluirFinanceiroRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/financeiros/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Financeiro?>>()
            ?? new Response<Financeiro?>(null, 400, "Falha ao excluir o financeiro.");
    }

    public async Task<Response<Financeiro?>> ListarFinanceiroPorIdAsync(ListarFinanceiroPorIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<Response<Financeiro?>>($"v1/financeiros/{request.Id}")
            ?? new Response<Financeiro?>(null, 400, "Não foi possível obter o financeiro.");
    }

    public async Task<PaginacaoResponse<List<Financeiro>?>> ListarTodosFinanceirosAsync(ListarTodosFinanceirosRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Financeiro>?>>($"v1/financeiros")
            ?? new PaginacaoResponse<List<Financeiro>?>(null, 400, "Não foi possível obter os financeiros.");
    }

    public async Task<PaginacaoResponse<List<Financeiro>?>> ListarFinanceirosPorContratoIdAsync(ListarFinanceirosPorContratoIdRequest request)
    {
        return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Financeiro>?>>($"v1/financeiros/{request.ContratoId}")
            ?? new PaginacaoResponse<List<Financeiro>?>(null, 400, "Não foi possível obter os financeiros por contrato.");
    }
}
