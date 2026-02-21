using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Curso;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cursos;

public class ListarPorCodigoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Cursos: Listar por código")
            .WithSummary("Listar um curso")
            .WithDescription("Listar um curso")
            .WithOrder(4)
            .Produces<Response<Curso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICursoHandler handler,
        int id)
    {
        var request = new ListarCursoPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarCursoPorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
