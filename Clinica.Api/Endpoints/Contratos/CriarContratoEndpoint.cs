using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Contratos;

public class CriarContratoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Contratos: Criar")
            .WithSummary("Criar um novo contrato")
            .WithDescription("Criar um novo contrato")
            .WithOrder(1)
            .Produces<Response<Contrato?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IContratoHandler handler,
        CriarContratoRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
