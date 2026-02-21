using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Paciente;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Pacientes
{
    public class ListarPacientePorIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
                .WithName("Pacientes: Listar por código")
                .WithSummary("Listar um paciente")
                .WithDescription("Listar um paciente")
                .WithOrder(4)
                .Produces<Response<Paciente?>>();
        }

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IPacienteHandler handler,
            int id)
        {
            var request = new ListarPacientePorIdRequest
            {
                Id = id
            };
            var result = await handler.ListarPacientePorIdAsync(request);

            return result.Sucesso
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
