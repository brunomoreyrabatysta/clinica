using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Financeiros;

public class AlterarFinanceiroEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Financeiros: Alterar")
            .WithSummary("Alterar um financeiro")
            .WithDescription("Alterar um financeiro")
            .WithOrder(2)
            .Produces<Response<Financeiro?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFinanceiroHandler handler,
        AlterarFinanceiroRequest request,
        int id)
    {
        request.Id = id;
        var result = await handler.AlterarAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
