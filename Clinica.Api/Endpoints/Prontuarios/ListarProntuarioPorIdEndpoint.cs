using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Prontuarios;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Prontuarios;

public class ListarProntuarioPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Prontuarios: Listar por código")
            .WithSummary("Listar um prontuário")
            .WithDescription("Listar um prontuário")
            .WithOrder(4)
            .Produces<Response<Prontuario?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProntuarioHandler handler,
        int id)
    {
        var request = new ListarProntuarioPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarProntuarioPorIdAsync(request);
        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
