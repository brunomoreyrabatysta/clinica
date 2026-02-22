using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Pacientes
{
    public class ExcluirPacienteEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync)
                .WithName("Pacientes: Excluir")
                .WithSummary("Excluir um paciente")
                .WithDescription("Excluir um paciente")
                .WithOrder(3)
                .Produces<Response<Paciente?>>();
        }

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IPacienteHandler handler,
            int id)
        {
            var request = new ExcluirPacienteRequest
            {
                Id = id
            };
            var result = await handler.ExcluirAsync(request);

            return result.Sucesso
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
