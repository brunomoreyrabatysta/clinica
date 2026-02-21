using Microsoft.AspNetCore.Http;
using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.TiposCurso;

public class CriarTipoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Tipo de cursos: Criar")
            .WithSummary("Criar um novo tipo de curso")
            .WithDescription("Criar um novo tipo de curso")
            .WithOrder(1)
            .Produces<Response<TipoCurso?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITipoCursoHandler handler,
        CriarTipoCursoRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
