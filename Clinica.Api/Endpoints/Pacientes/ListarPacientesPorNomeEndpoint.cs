using Clinica.Api.Common.Api;
using Clinica.Core;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Paciente;
using Clinica.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Pacientes
{
    public class ListarPacientesPorNomeEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{nome}", HandleAsync)
                .WithName("Pacientes: Listar todo(s) paciente(s) por nome")
                .WithSummary("Listar todo(s) paciente(s) por nome")
                .WithDescription("Listar todo(s) paciente(s) por nome")
                .WithOrder(6)
                .Produces<PaginacaoResponse<List<Paciente>?>>();
        }

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IPacienteHandler handler,
            [FromQuery] string nome,
            [FromQuery] int numeroPagina = Configuracao.PadraoNumeroPagina,
            [FromQuery] int tamanhoPagina = Configuracao.PadraoTamanhoPagina)
        {
            var request = new ListarPacientesPorNomeRequest
            {
                NumeroPagina = numeroPagina,
                TamanhoPagina = tamanhoPagina,
                Nome = nome
            };
            var result = await handler.ListarPacientesPorNomeAsync(request);

            return result.Sucesso
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
