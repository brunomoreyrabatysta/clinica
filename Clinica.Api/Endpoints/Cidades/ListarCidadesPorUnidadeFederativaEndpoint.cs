using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cidades;

public class ListarCidadesPorUnidadeFederativaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{unidadefederativaid}", HandleAsync)
            .WithName("Cidades: Listar toda(s) cidade(s) por unidade federativa")
            .WithSummary("Listar toda(s) cidade(s) por unidade federativa")
            .WithDescription("Listar toda(s) cidade(s) por unidade federativa")
            .WithOrder(7)
            .Produces<PaginacaoResponse<List<Cidade>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICidadeHandler handler,
        [FromQuery] long unidadeFederativaId,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarCidadesPorUnidadeFederativaRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            UnidadeFederativaId = unidadeFederativaId
        };
        var result = await handler.ListarCidadesPorUnidadeFederativaAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
