using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Financeiros;

public class ListarFinanceiroPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Financeiros: Listar por código")
            .WithSummary("Listar um financeiro")
            .WithDescription("Listar um financeiro")
            .WithOrder(4)
            .Produces<Response<Financeiro?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFinanceiroHandler handler,
        int id)
    {
        var request = new ListarFinanceiroPorIdRequest
        {
            Id = id
        };
        var result = await handler.ListarFinanceiroPorIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
