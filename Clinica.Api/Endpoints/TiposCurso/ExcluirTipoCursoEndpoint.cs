using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.TiposCurso;

public class ExcluirTipoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Tipo de cursos: Excluir")
            .WithSummary("Excluir um tipo de curso")
            .WithDescription("Excluir um tipo de curso")
            .WithOrder(3)
            .Produces<Response<TipoCurso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITipoCursoHandler handler,
        int id)
    {
        var request = new ExcluirTipoCursoRequest
        {
            Id = id
        };
        var result = await handler.ExcluirAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}

