using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Responsaveis;

public class ExcluirResponsavelEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Responsáveis: Excluir")
            .WithSummary("Excluir um responsavel")
            .WithDescription("Excluir um responsavel")
            .WithOrder(3)
            .Produces<Response<Responsavel?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IResponsavelHandler handler,
        int id)
    {
        var request = new ExcluirResponsavelRequest
        {
            Id = id
        };
        var result = await handler.ExcluirAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
