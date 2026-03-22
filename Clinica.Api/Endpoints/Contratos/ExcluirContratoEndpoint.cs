using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Contratos;

public class ExcluirContratoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Contratos: Excluir")
            .WithSummary("Excluir um contrato")
            .WithDescription("Excluir um contrato")
            .WithOrder(3)
            .Produces<Response<Contrato?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IContratoHandler handler,
        int id)
    {
        var request = new ExcluirContratoRequest
        {
            Id = id
        };
        var result = await handler.ExcluirAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
