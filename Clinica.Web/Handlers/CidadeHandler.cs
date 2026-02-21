using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidade;
using Clinica.Core.Responses;
using System.Net.Http.Json;

namespace Clinica.Web.Handlers
{
    public class CidadeHandler(IHttpClientFactory httpClientFactory) : ICidadeHandler
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<Cidade?>> AlterarAsync(AlterarCidadeRequest request)
        {
            var result = await _httpClient.PutAsJsonAsync($"v1/cidades/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Response<Cidade?>>()
                ?? new Response<Cidade?>(null, 400, "Falha ao atualizar a cidade.");
        }

        public async Task<Response<Cidade?>> CriarAsync(CriarCidadeRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("v1/cidades", request);
            return await result.Content.ReadFromJsonAsync<Response<Cidade?>>()
                ?? new Response<Cidade?>(null, 400, "Falha ao criar a cidade.");
        }

        public async Task<Response<Cidade?>> ExcluirAsync(ExcluirCidadeRequest request)
        {
            var result = await _httpClient.DeleteAsync($"v1/cidades/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Cidade?>>()
                ?? new Response<Cidade?>(null, 400, "Falha ao excluir a cidade.");
        }

        public async Task<Response<Cidade?>> ListarCidadePorIdAsync(ListarCidadePorIdRequest request)
        {
            return await _httpClient.GetFromJsonAsync<Response<Cidade?>>($"v1/cidades/{request.Id}")
                ?? new Response<Cidade?>(null, 400, "Não foi possível obter a cidade.");
        }

        public async Task<PaginacaoResponse<List<Cidade>?>> ListarTodasCidadesAsync(ListarTodasCidadesRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Cidade>?>>($"v1/cidades")
                ?? new PaginacaoResponse<List<Cidade>?>(null, 400, "Não foi possível obter as cidades.");
        }

        public async Task<PaginacaoResponse<List<Cidade>?>> ListarCidadesPorNomeAsync(ListarCidadesPorNomeRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Cidade>?>>($"v1/cidades/{request.Nome}")
                ?? new PaginacaoResponse<List<Cidade>?>(null, 400, "Não foi possível obter as cidades por nome.");
        }

        public async Task<PaginacaoResponse<List<Cidade>?>> ListarCidadesPorUnidadeFederativaAsync(ListarCidadesPorUnidadeFederativaRequest request)
        {
            return await _httpClient.GetFromJsonAsync<PaginacaoResponse<List<Cidade>?>>($"v1/cidades/{request.UnidadeFederativaId}")
                ?? new PaginacaoResponse<List<Cidade>?>(null, 400, "Não foi possível obter as cidades por unidade federativa.");
        }
    }
}
