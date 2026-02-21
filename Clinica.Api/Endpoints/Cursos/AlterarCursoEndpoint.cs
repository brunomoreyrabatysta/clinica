using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Curso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cursos;

public class AlterarCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Cursos: Alterar")
            .WithSummary("Alterar um curso")
            .WithDescription("Alterar um curso")
            .WithOrder(2)
            .Produces<Response<Curso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICursoHandler handler,
        AlterarCursoRequest request,
        int id)
    {
        request.Id = id;
        var result = await handler.AlterarAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
