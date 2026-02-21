namespace Clinica.Api.Common.Api;

public static class AppExntesion
{
    public static void ConfiguracaoDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapSwagger().RequireAuthorization();
    }

    public static void UtilizacaoSeguranca(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
