using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Curso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cursos;

public class ExcluirCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Cursos: Excluir")
            .WithSummary("Excluir um curso")
            .WithDescription("Excluir um curso")
            .WithOrder(3)
            .Produces<Response<Curso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICursoHandler handler,
        int id)
    {
        var request = new ExcluirCursoRequest
        {
            Id = id
        };
        var result = await handler.ExcluirAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
