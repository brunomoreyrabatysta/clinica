using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadeFederativa;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.UnidadesFederativas;

public class ListarUnidadesFederativasPorSiglaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{sigla}", HandleAsync)
            .WithName("Unidade Federativa: Listar toda(s) unidade(s) federativa(s) por sigla")
            .WithSummary("Listar toda(s) unidade(s) federativa(s) por sigla")
            .WithDescription("Listar toda(s) unidade(s) federativa(s) por sigla")
            .WithOrder(7)
            .Produces<PaginacaoResponse<List<UnidadeFederativa>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeFederativaHandler handler,
        [FromQuery] string sigla,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarUnidadesFederativasPorSiglaRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            Sigla = sigla
        };
        var result = await handler.ListarUnidadesFederativasPorSiglaAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
    