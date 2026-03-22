using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Financeiros;

public class ListarTodosFinanceirosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Financeiros: Listar todo(s) financeiro(s)")
            .WithSummary("Listar todo(s) financeiro(s)")
            .WithDescription("Listar todo(s) financeiro(s)")
            .WithOrder(5)
            .Produces<PaginacaoResponse<List<Financeiro>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IFinanceiroHandler handler,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarTodosFinanceirosRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina
        };
        var result = await handler.ListarTodosFinanceirosAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
