using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Curso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cursos;

public class CriarCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Cursos: Criar")
            .WithSummary("Criar um novo curso")
            .WithDescription("Criar um novo curso")
            .WithOrder(1)
            .Produces<Response<Curso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICursoHandler handler,
        CriarCursoRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
