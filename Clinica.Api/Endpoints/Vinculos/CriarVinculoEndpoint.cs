using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Vinculos;

public class CriarVinculoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Vinculos: Criar")
            .WithSummary("Criar um novo vínculo")
            .WithDescription("Criar um novo vínculo")
            .WithOrder(1)
            .Produces<Response<Vinculo?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IVinculoHandler handler,
        CriarVinculoRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
