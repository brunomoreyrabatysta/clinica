using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cidades;

public class ListarCidadePorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Cidades: Listar por código")
            .WithSummary("Listar uma cidade")
            .WithDescription("Listar uma cidade")
            .WithOrder(4)
            .Produces<Response<Cidade?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICidadeHandler handler,
        int id)
    {
        var request = new ListarCidadePorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarCidadePorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
