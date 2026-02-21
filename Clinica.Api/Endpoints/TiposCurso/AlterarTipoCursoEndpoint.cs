using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.TiposCurso;

public class AlterarTipoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Tipo de cursos: Alterar")
            .WithSummary("Alterar um tipo de curso")
            .WithDescription("Alterar um tipo de curso")
            .WithOrder(2)
            .Produces<Response<TipoCurso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITipoCursoHandler handler,
        AlterarTipoCursoRequest request,
        int id)
    {
        request.Id = id;
        var result = await handler.AlterarAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
