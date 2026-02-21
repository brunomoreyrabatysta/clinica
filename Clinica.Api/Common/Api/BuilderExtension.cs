using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Clinica.Api.Data;
using Clinica.Api.Handlers;
using Clinica.Api.Models;
using Clinica.Core;
using Clinica.Core.Handlers;

namespace Clinica.Api.Common.Api;

public static class BuilderExtension
{
    public static void AdicionarConfiguracao(this WebApplicationBuilder builder)
    {
        Configuracao.StringConexao = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuracao.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuracao.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }

    public static void AdicionarDocumentacao(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });
    }

    public static void AdicionarSeguranca(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        builder.Services.AddAuthorization();
    }

    public static void AdicionarContextos(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(
            options =>
            {
                options.UseNpgsql(Configuracao.StringConexao);
            });

        builder.Services
            .AddIdentityCore<Usuario>()
            .AddRoles<IdentityRole<long>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguracao.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuracao.BackendUrl,
                        Configuracao.FrontendUrl
                    ])                    
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
    }

    public static void AdicionarServicos(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ISituacaoCursoHandler, SituacaoCursoHandler>();
        builder.Services.AddTransient<ITipoCursoHandler, TipoCursoHandler>();
        builder.Services.AddTransient<ICursoHandler, CursoHandler>();
        builder.Services.AddTransient<IUnidadeFederativaHandler, UnidadeFederativaHandler>();
        builder.Services.AddTransient<ICidadeHandler, CidadeHandler>();
        builder.Services.AddTransient<IPacienteHandler, PacienteHandler>();
    }
}
