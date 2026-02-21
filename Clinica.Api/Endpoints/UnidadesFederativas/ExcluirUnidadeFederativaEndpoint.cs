using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadeFederativa;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.UnidadesFederativas;

public class ExcluirUnidadeFederativaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Unidade Federativa: Excluir")
            .WithSummary("Excluir uma unidade federativa")
            .WithDescription("Excluir uma unidade federativa")
            .WithOrder(3)
            .Produces<Response<UnidadeFederativa?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeFederativaHandler handler,
        int id)
    {
        var request = new ExcluirUnidadeFederativaRequest
        {
            Id = id
        };
        var result = await handler.ExcluirAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
