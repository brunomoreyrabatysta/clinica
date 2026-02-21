using Clinica.Api.Common.Api;
using Clinica.Api.Endpoints.TiposCurso;
using Clinica.Api.Endpoints.Cursos;
using Clinica.Api.Models;
using Clinica.Api.Endpoints.Identity;
using Clinica.Api.Endpoints.SituacoesCurso;
using Clinica.Api.Endpoints.UnidadesFederativas;
using Clinica.Api.Endpoints.Cidades;
using Clinica.Api.Endpoints.Pacientes;

namespace Clinica.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endPoints = app
                .MapGroup("");
            
            endPoints.MapGroup("/")
                .WithTags("Checagem de saúde")
                .MapGet("/", () => new { mensagem = "OK" });

            endPoints
                .MapGroup("v1/tiposcurso")
                .WithTags("TiposCurso")
                .RequireAuthorization()
                .MapEndpoint<CriarTipoCursoEndpoint>()
                .MapEndpoint<AlterarTipoCursoEndpoint>()
                .MapEndpoint<ExcluirTipoCursoEndpoint>()
                .MapEndpoint<ListarPorIdTipoCursoEndpoint>()
                .MapEndpoint<ListarTodosTipoCursoEndpoint>();

            endPoints
                .MapGroup("v1/situacoescurso")
                .WithTags("SituacoesCurso")
                .RequireAuthorization()
                .MapEndpoint<CriarSituacaoCursoEndpoint>()
                .MapEndpoint<AlterarSituacaoCursoEndpoint>()
                .MapEndpoint<ExcluirSituacaoCursoEndpoint>()
                .MapEndpoint<ListarPorIdSituacaoCursoEndpoint>()
                .MapEndpoint<ListarTodasSituacaoCursoEndpoint>();

            endPoints
                .MapGroup("v1/cursos")
                .WithTags("Cursos")
                .RequireAuthorization()
                .MapEndpoint<CriarCursoEndpoint>()
                .MapEndpoint<AlterarCursoEndpoint>()
                .MapEndpoint<ExcluirCursoEndpoint>()
                .MapEndpoint<ListarPorCodigoCursoEndpoint>()
                .MapEndpoint<ListarTodosCursoEndpoint>();
                        
            endPoints
                .MapGroup("v1/unidadesfederativas")
                .WithTags("UnidadesFederativas")
                .RequireAuthorization()
                .MapEndpoint<CriarUnidadeFederativaEndpoint>()
                .MapEndpoint<AlterarUnidadeFederativaEndpoint>()
                .MapEndpoint<ExcluirUnidadeFederativaEndpoint>()
                .MapEndpoint<ListarUnidadeFederativaPorIdEndpoint>()
                .MapEndpoint<ListarUnidadesFederativasPorNomeEndpoint>()
                .MapEndpoint<ListarUnidadesFederativasPorSiglaEndpoint>()
                .MapEndpoint<ListarTodasUnidadesFederativasEndpoint>();

            endPoints
                .MapGroup("v1/cidades")
                .WithTags("Cidades")
                .RequireAuthorization()
                .MapEndpoint<CriarCidadeEndpoint>()
                .MapEndpoint<AlterarCidadeEndpoint>()
                .MapEndpoint<ExcluirCidadeEndpoint>()
                .MapEndpoint<ListarCidadePorIdEndpoint>()
                .MapEndpoint<ListarCidadesPorNomeEndpoint>()
                .MapEndpoint<ListarCidadesPorUnidadeFederativaEndpoint>()
                .MapEndpoint<ListarTodasCidadesEndpoint>();

            endPoints
                .MapGroup("v1/pacientes")
                .WithTags("Pacientes")
                .RequireAuthorization()
                .MapEndpoint<CriarPacienteEndpoint>()
                .MapEndpoint<AlterarPacienteEndpoint>()
                .MapEndpoint<ExcluirPacienteEndpoint>()
                .MapEndpoint<ListarPacientePorIdEndpoint>()
                .MapEndpoint<ListarPacientesPorNomeEndpoint>()                
                .MapEndpoint<ListarTodosPacientesEndpoint>();

            endPoints
                .MapGroup("v1/identity")
                .WithTags("Identity")
                .MapIdentityApi<Usuario>();

            endPoints
                .MapGroup("v1/identity")
                .WithTags("Identity")
                .MapEndpoint<LogoutEndpoint>()
                .MapEndpoint<GetRolesEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
