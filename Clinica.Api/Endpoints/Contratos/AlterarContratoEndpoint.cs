using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Contratos;

public class AlterarContratoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Contratos: Alterar")
            .WithSummary("Alterar um contrato")
            .WithDescription("Alterar um contrato")
            .WithOrder(2)
            .Produces<Response<Contrato?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IContratoHandler handler,
        AlterarContratoRequest request,
        int id)
    {
        request.Id = id;
        var result = await handler.AlterarAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}