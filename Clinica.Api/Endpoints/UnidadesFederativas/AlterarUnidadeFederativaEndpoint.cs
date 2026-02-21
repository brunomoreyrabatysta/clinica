using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadeFederativa;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.UnidadesFederativas;

public class AlterarUnidadeFederativaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Unidade Federativa: Alterar")
            .WithSummary("Alterar uma unidade federativa")
            .WithDescription("Alterar uma unidade federativa")
            .WithOrder(2)
            .Produces<Response<UnidadeFederativa?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeFederativaHandler handler,
        AlterarUnidadeFederativaRequest request,
        int id)
    {
        request.Id = id;
        var result = await handler.AlterarAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
