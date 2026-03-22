using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Vinculos;

public class ListarVinculosPorNomeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{nome}", HandleAsync)
            .WithName("Vinculos: Listar todo(s) vínculo(s) por nome")
            .WithSummary("Listar todo(s) vínculo(s) por nome")
            .WithDescription("Listar todo(s) vínculo(s) por nome")
            .WithOrder(6)
            .Produces<PaginacaoResponse<List<Vinculo>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IVinculoHandler handler,
        [FromQuery] string nome,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarVinculosPorNomeRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            Nome = nome
        };
        var result = await handler.ListarVinculosPorNomeAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
