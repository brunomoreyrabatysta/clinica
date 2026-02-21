using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.SituacaoCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.SituacoesCurso;

public class CriarSituacaoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Situação de cursos: Criar")
            .WithSummary("Criar uma nova situação de curso")
            .WithDescription("Criar uma nova situação de curso")
            .WithOrder(1)
            .Produces<Response<SituacaoCurso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISituacaoCursoHandler handler,
        CriarSituacaoCursoRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
