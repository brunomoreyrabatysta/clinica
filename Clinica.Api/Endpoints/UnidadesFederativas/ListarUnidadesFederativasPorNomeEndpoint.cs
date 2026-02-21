using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadeFederativa;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.UnidadesFederativas;

public class ListarUnidadesFederativasPorNomeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{nome}", HandleAsync)
            .WithName("Unidade Federativa: Listar toda(s) unidade(s) federativa(s) por nome")
            .WithSummary("Listar toda(s) unidade(s) federativa(s) por nome")
            .WithDescription("Listar toda(s) unidade(s) federativa(s) por nome")
            .WithOrder(6)
            .Produces<PaginacaoResponse<List<UnidadeFederativa>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeFederativaHandler handler,
        [FromQuery] string nome,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarUnidadesFederativasPorNomeRequest
        {   
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            Nome = nome
        };
        var result = await handler.ListarUnidadesFederativasPorNomeAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
