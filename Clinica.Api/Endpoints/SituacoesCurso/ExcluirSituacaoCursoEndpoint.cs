using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.SituacaoCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.SituacoesCurso;

public class ExcluirSituacaoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Situação de cursos: Excluir")
            .WithSummary("Excluir uma situação de curso")
            .WithDescription("Excluir uma situação de curso")
            .WithOrder(3)
            .Produces<Response<SituacaoCurso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISituacaoCursoHandler handler,
        int id)
    {
        var request = new ExcluirSituacaoCursoRequest
        {
            Id = id
        };
        var result = await handler.ExcluirAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
