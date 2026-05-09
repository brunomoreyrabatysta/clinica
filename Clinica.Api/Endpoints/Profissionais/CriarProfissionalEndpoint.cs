using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Profissionais;

public class CriarProfissionalEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Profissionais: Criar")
            .WithSummary("Criar um novo profissional")
            .WithDescription("Criar um novo profissional")
            .WithOrder(1)
            .Produces<Response<Profissional?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProfissionalHandler handler,
        CriarProfissionalRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
