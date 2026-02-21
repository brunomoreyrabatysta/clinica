using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.SituacaoCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.SituacoesCurso;

public class AlterarSituacaoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Situação de cursos: Alterar")
            .WithSummary("Alterar uma situação de curso")
            .WithDescription("Alterar uma situação de curso")
            .WithOrder(2)
            .Produces<Response<SituacaoCurso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISituacaoCursoHandler handler,
        AlterarSituacaoCursoRequest request,
        int id)
    {
        request.Id = id;
        var result = await handler.AlterarAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
