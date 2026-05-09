using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Profissionais;

public class ListarProfissionalPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Profissionais: Listar por código")
            .WithSummary("Listar um profissional")
            .WithDescription("Listar um profissional")
            .WithOrder(4)
            .Produces<Response<Profissional?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProfissionalHandler handler,
        int id)
    {
        var request = new ListarProfissionalPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarProfissionalPorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
