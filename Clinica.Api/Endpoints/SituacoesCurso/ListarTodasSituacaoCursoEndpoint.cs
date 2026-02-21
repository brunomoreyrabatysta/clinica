using Microsoft.AspNetCore.Mvc;
using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.SituacaoCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.SituacoesCurso;

public class ListarTodasSituacaoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        //app.MapGet("/{tamanhoPagina}/{numeroPagina}", HandleAsync)
        app.MapGet("/", HandleAsync)
            .WithName("Situação de cursos: Listar todos")
            .WithSummary("Listar todas situações de curso")
            .WithDescription("Listar todas situações de curso")
            .WithOrder(5)
            .Produces<PaginacaoResponse<List<SituacaoCurso>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ISituacaoCursoHandler handler,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarTodasSituacaoCursoRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina
        };
        var result = await handler.ListarTodasAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
