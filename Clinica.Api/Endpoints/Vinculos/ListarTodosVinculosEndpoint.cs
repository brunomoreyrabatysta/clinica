using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Vinculos;

public class ListarTodosVinculosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Vinculos: Listar todo(s) vínculo(s)")
            .WithSummary("Listar todo(s) vínculo(s)")
            .WithDescription("Listar todo(s) vínculo(s)")
            .WithOrder(5)
            .Produces<PaginacaoResponse<List<Vinculo>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IVinculoHandler handler,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarTodosVinculosRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina
        };
        var result = await handler.ListarTodosVinculosAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
