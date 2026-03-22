using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Financeiros;

public class CriarFinanceiroEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Financeiros: Criar")
            .WithSummary("Criar um novo financeiro")
            .WithDescription("Criar um novo financeiro")
            .WithOrder(1)
            .Produces<Response<Financeiro?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFinanceiroHandler handler,
        CriarFinanceiroRequest request)
    {
        var result = await handler.CriarAsync(request);

        return result.Sucesso
            ? TypedResults.Created($"/{result.Dados?.Id}", result)
            : TypedResults.BadRequest(result);
    }
}
