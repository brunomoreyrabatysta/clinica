using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.SituacaoCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.SituacoesCurso;

public class ListarPorIdSituacaoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Situação de cursos: Listar por Id (código)")
            .WithSummary("Listar uma situação de curso")
            .WithDescription("Listar uma situação de curso")
            .WithOrder(4)
            .Produces<Response<SituacaoCurso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISituacaoCursoHandler handler,
        int id)
    {
        var request = new ListarSituacaoCursoPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarPorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
