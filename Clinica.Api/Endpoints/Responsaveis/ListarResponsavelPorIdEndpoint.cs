using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Responsaveis;

public class ListarResponsavelPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Responsáveis: Listar por código")
            .WithSummary("Listar um responsável")
            .WithDescription("Listar um responsável")
            .WithOrder(4)
            .Produces<Response<Responsavel?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IResponsavelHandler handler,
        int id)
    {
        var request = new ListarResponsavelPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarResponsavelPorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
