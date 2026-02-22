using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadesFederativas;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.UnidadesFederativas;

public class ListarTodasUnidadesFederativasEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        //app.MapGet("/{tamanhoPagina}/{numeroPagina}", HandleAsync)
        app.MapGet("/", HandleAsync)
            .WithName("Unidade Federativa: Listar toda(s) unidade(s) federativa(s)")
            .WithSummary("Listar toda(s) unidade(s) federativa(s)")
            .WithDescription("Listar toda(s) unidade(s) federativa(s)")
            .WithOrder(5)
            .Produces<PaginacaoResponse<List<UnidadeFederativa>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeFederativaHandler handler,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarTodasUnidadesFederativasRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina
        };
        var result = await handler.ListarTodasUnidadesFederativasAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
