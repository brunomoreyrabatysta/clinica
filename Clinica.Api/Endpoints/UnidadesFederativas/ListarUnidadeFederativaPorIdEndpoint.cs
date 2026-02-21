using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadeFederativa;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.UnidadesFederativas;

public class ListarUnidadeFederativaPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Unidade Federativa: Listar por código")
            .WithSummary("Listar uma unidade federativa")
            .WithDescription("Listar uma unidade federativa")
            .WithOrder(4)
            .Produces<Response<UnidadeFederativa?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeFederativaHandler handler,
        int id)
    {
        var request = new ListarUnidadeFederativaPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarUnidadeFederativaPorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
