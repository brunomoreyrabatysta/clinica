using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Pacientes
{
    public class AlterarPacienteEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandleAsync)
                .WithName("Pacientes: Alterar")
                .WithSummary("Alterar um paciente")
                .WithDescription("Alterar um paciente")
                .WithOrder(2)
                .Produces<Response<Paciente?>>();
        }

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IPacienteHandler handler,
            AlterarPacienteRequest request,
            int id)
        {
            request.Id = id;
            var result = await handler.AlterarAsync(request);

            return result.Sucesso
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
