using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Responsaveis;

public class ListarResponsaveisPorNomeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{nome}", HandleAsync)
            .WithName("Responsáveis: Listar todo(s) responsável(is) por nome")
            .WithSummary("Listar todo(s) responsável(is) por nome")
            .WithDescription("Listar todo(s) responsável(s) por nome")
            .WithOrder(6)
            .Produces<PaginacaoResponse<List<Responsavel>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IResponsavelHandler handler,
        [FromQuery] string nome,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarResponsaveisPorNomeRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            Nome = nome
        };
        var result = await handler.ListarResponsaveisPorNomeAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
