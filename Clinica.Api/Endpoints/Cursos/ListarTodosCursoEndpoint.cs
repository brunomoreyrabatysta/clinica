using Microsoft.AspNetCore.Mvc;
using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;
using Clinica.Core;
using Clinica.Core.Requests.Curso;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Cursos;

public class ListarTodosCursoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        //app.MapGet("/{tamanhoPagina}/{numeroPagina}", HandleAsync)
        app.MapGet("/", HandleAsync)
            .WithName("Cursos: Listar todos cursos")
            .WithSummary("Listar todos cursos")
            .WithDescription("Listar todos cursos")
            .WithOrder(5)
            .Produces<PaginacaoResponse<List<Curso>?>>();
    }

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        ICursoHandler handler,
        [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
        [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
    {
        var request = new ListarTodosCursoRequest
        {
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina
        };
        var result = await handler.ListarTodosCursoAsync(request);

        return result.Sucesso
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
