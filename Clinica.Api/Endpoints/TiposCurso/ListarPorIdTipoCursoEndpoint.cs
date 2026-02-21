using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.TiposCurso;

public class ListarPorIdTipoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Tipo de cursos: Listar por Id (código)")
            .WithSummary("Listar um tipo de curso")
            .WithDescription("Listar um tipo de curso")
            .WithOrder(4)
            .Produces<Response<TipoCurso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITipoCursoHandler handler,
        int id)
    {
        var request = new ListarTipoCursoPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarPorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}

