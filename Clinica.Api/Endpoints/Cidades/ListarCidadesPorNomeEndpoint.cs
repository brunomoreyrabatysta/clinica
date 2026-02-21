using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidade;
using Clinica.Core.Requests.UnidadeFederativa;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cidades;

public class ListarCidadesPorNomeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{nome}", HandleAsync)
            .WithName("Cidades: Listar toda(s) cidade(s) por nome")
            .WithSummary("Listar toda(s) cidade(s) por nome")
            .WithDescription("Listar toda(s) cidade(s) por nome")
            .WithOrder(6)
            .Produces<PaginacaoResponse<List<Cidade>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICidadeHandler handler,
        [FromQuery] string nome,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarCidadesPorNomeRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            Nome = nome
        };
        var result = await handler.ListarCidadesPorNomeAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
