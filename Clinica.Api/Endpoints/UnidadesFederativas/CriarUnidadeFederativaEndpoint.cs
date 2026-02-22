using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadesFederativas;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.UnidadesFederativas;

public class CriarUnidadeFederativaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Unidade Federativa: Criar")
            .WithSummary("Criar uma nova unidade federativa")
            .WithDescription("Criar uma nova unidade federativa")
            .WithOrder(1)
            .Produces<Response<UnidadeFederativa?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUnidadeFederativaHandler handler,
        CriarUnidadeFederativaRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
