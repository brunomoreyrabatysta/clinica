using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Profissionais;

public class AlterarProfissionalEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}", HandleAsync)
            .WithName("Profissionais: Alterar")
            .WithSummary("Alterar um profissional")
            .WithDescription("Alterar um profissional")
            .WithOrder(2)
            .Produces<Response<Profissional?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProfissionalHandler handler,
        AlterarProfissionalRequest request,
        int id)
    {
        request.Id = id;
        var result = await handler.AlterarAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
