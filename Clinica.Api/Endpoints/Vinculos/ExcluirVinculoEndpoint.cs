using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Vinculos;

public class ExcluirVinculoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Vinculos: Excluir")
            .WithSummary("Excluir um vínculo")
            .WithDescription("Excluir um vínculo")
            .WithOrder(3)
            .Produces<Response<Vinculo?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IVinculoHandler handler,
        int id)
    {
        var request = new ExcluirVinculoRequest
        {
            Id = id
        };
        var result = await handler.ExcluirAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}