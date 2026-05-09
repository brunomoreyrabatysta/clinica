using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Profissionais;

public class ListarProfissionaisPorNomeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{nome}", HandleAsync)
            .WithName("Profissionais: Listar todo(s) profissional(is) por nome")
            .WithSummary("Listar todo(s) profissional(is) por nome")
            .WithDescription("Listar todo(s) profissional(is) por nome")
            .WithOrder(6)
            .Produces<PaginacaoResponse<List<Profissional>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IProfissionalHandler handler,
        [FromQuery] string nome,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarProfissionaisPorNomeRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            Nome = nome
        };
        var result = await handler.ListarProfissionaisPorNomeAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}