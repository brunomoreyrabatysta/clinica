using Clinica.Api.Common.Api;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Responses;
using System.Security.Claims;

namespace Clinica.Api.Endpoints.Pacientes
{
    public class CriarPacienteEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
                .WithName("Pacientes: Criar")
                .WithSummary("Criar um novo paciente")
                .WithDescription("Criar um novo paciente")
                .WithOrder(1)
                .Produces<Response<Paciente?>>();
        }

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IPacienteHandler handler,
            CriarPacienteRequest request)
        {
            var result = await handler.CriarAsync(request);

            return result.Sucesso
                ? TypedResults.Created($"/{result.Dados?.Id}", result)
                : TypedResults.BadRequest(result);
        }
    }
}
