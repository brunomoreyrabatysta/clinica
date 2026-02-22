using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cidades;

public class ListarTodasCidadesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        //app.MapGet("/{tamanhoPagina}/{numeroPagina}", HandleAsync)
        app.MapGet("/", HandleAsync)
            .WithName("Cidades: Listar toda(s) cidade(s)")
            .WithSummary("Listar toda(s) cidade(s)")
            .WithDescription("Listar toda(s) cidade(s)")
            .WithOrder(5)
            .Produces<PaginacaoResponse<List<Cidade>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICidadeHandler handler,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarTodasCidadesRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina
        };
        var result = await handler.ListarTodasCidadesAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
