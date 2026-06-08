using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Prontuarios;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Prontuarios;

public class CriarProntuarioEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Prontuarios: Criar")
            .WithSummary("Criar um novo prontuário")
            .WithDescription("Criar um novo prontuário")
            .WithOrder(1)
            .Produces<Response<Prontuario?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProntuarioHandler handler,
        CriarProntuarioRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
