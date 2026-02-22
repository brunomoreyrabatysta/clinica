using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Pacientes
{
    public class ListarTodosPacientesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            //app.MapGet("/{tamanhoPagina}/{numeroPagina}", HandleAsync)
            app.MapGet("/", HandleAsync)
                .WithName("Pacientes: Listar todo(s) paciente(s)")
                .WithSummary("Listar todo(s) paciente(s)")
                .WithDescription("Listar todo(s) paciente(s)")
                .WithOrder(5)
                .Produces<PaginacaoResponse<List<Paciente>?>>();
        }

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IPacienteHandler handler,
            [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
            [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
        {
            var request = new ListarTodosPacientesRequest
            {
                NumeroPagina = numeroPagina,
                TamanhoPagina = tamanhoPagina
            };
            var result = await handler.ListarTodosPacientesAsync(request);

            return result.Sucesso
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
