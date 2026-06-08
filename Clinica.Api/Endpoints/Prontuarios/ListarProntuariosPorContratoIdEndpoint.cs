using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Prontuarios;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Prontuarios;

public class ListarProntuariosPorContratoIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{contratoId}", HandleAsync)
            .WithName("Prontuários: Listar todo(s) profissional(is) por contrato")
            .WithSummary("Listar todo(s) profissional(is) por contrato")
            .WithDescription("Listar todo(s) profissional(is) por contrato")
            .WithOrder(6)
            .Produces<PaginacaoResponse<List<Prontuario>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProntuarioHandler handler,
        [FromQuery] long contratoId,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarProntuariosPorContratoIdRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            ContratoId = contratoId
        };
        var result = await handler.ListarProntuariosPorContratoIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
