using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Contratos;

public class ListarContratoPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Contratos: Listar por código")
            .WithSummary("Listar um contrato")
            .WithDescription("Listar um contrato")
            .WithOrder(4)
            .Produces<Response<Contrato?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IContratoHandler handler,
        int id)
    {
        var request = new ListarContratoPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarContratoPorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
