namespace Clinica.Core;

public static class Configuracao
{
    public const int PadraoNumeroPagina = 1;
    public const int PadraoTamanhoPagina = 25;

    public const int PadraoCodigoSituacao = 200;

    public static string StringConexao { get; set; } = string.Empty;
    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;
}
