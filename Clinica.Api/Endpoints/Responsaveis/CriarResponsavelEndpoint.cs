using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Responsaveis;

public class CriarResponsavelEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Responsáveis: Criar")
            .WithSummary("Criar um novo responsável")
            .WithDescription("Criar um novo responsável")
            .WithOrder(1)
            .Produces<Response<Responsavel?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IResponsavelHandler handler,
        CriarResponsavelRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
