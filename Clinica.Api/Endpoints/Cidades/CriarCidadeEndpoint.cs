using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cidades;

public class CriarCidadeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Cidades: Criar")
            .WithSummary("Criar uma nova cidade")
            .WithDescription("Criar uma nova cidade")
            .WithOrder(1)
            .Produces<Response<Cidade?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICidadeHandler handler,
        CriarCidadeRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
