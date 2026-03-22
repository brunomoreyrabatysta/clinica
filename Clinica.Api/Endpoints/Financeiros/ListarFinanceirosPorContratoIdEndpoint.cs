using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Financeiros;

public class ListarFinanceirosPorContratoIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{nome}", HandleAsync)
            .WithName("Financeiros: Listar todo(s) financeiro(s) por contrato")
            .WithSummary("Listar todo(s) financeiro(s) por contrato")
            .WithDescription("Listar todo(s) financeiro(s) por contrato")
            .WithOrder(6)
            .Produces<PaginacaoResponse<List<Financeiro>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFinanceiroHandler handler,
        [FromQuery] long contratoId,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarFinanceirosPorContratoIdRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            ContratoId = contratoId
        };
        var result = await handler.ListarFinanceirosPorContratoIdAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
