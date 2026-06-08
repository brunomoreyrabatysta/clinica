using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Prontuarios;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Prontuarios;

public class AlterarProntuarioEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Prontuarios: Alterar")
            .WithSummary("Alterar um prontuário")
            .WithDescription("Alterar um prontuário")
            .WithOrder(2)
            .Produces<Response<Prontuario?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProntuarioHandler handler,
        AlterarProntuarioRequest request,
        int id)
    {
        request.Id = id;
        var result = await handler.AlterarAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
