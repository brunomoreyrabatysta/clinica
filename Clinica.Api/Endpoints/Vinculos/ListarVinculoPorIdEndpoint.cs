using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Vinculos;

public class ListarVinculoPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Vinculos: Listar por código")
            .WithSummary("Listar um vínculo")
            .WithDescription("Listar um vínculo")
            .WithOrder(4)
            .Produces<Response<Vinculo?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IVinculoHandler handler,
        int id)
    {
        var request = new ListarVinculoPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarVinculoPorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
