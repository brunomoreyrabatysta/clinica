using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Prontuarios;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Prontuarios;

public class ExcluirProntuarioEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Prontuarios: Excluir")
            .WithSummary("Excluir um prontuário")
            .WithDescription("Excluir um prontuário")
            .WithOrder(3)
            .Produces<Response<Prontuario?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProntuarioHandler handler,
        int id)
    {
        var request = new ExcluirProntuarioRequest
        {
            Id = id
        };
        var result = await handler.ExcluirAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
