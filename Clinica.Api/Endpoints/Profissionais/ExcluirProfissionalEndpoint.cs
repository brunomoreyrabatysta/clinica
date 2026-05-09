using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Profissionais;

public class ExcluirProfissionalEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Profissionais: Excluir")
            .WithSummary("Excluir um profissional")
            .WithDescription("Excluir um profissional")
            .WithOrder(3)
            .Produces<Response<Profissional?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProfissionalHandler handler,
        int id)
    {
        var request = new ExcluirProfissionalRequest
        {
            Id = id
        };
        var result = await handler.ExcluirAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
