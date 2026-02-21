using Microsoft.AspNetCore.Mvc;
using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.TiposCurso;

public class ListarTodosTipoCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        //app.MapGet("/{tamanhoPagina}/{numeroPagina}", HandleAsync)
        app.MapGet("/", HandleAsync)
            .WithName("Tipo de cursos: Listar todos")
            .WithSummary("Listar todos tipos de curso")
            .WithDescription("Listar todos tipos de curso")
            .WithOrder(5)
            .Produces<PaginacaoResponse<List<TipoCurso>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ITipoCursoHandler handler,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarTodosTipoCursoRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina
        };
        var result = await handler.ListarTodosAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}

