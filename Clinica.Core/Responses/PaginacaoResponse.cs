using System.Text.Json.Serialization;

namespace Clinica.Core.Responses;

public class PaginacaoResponse<TDado> : Response<TDado>
{
    [JsonConstructor]
    public PaginacaoResponse(
        TDado? dados,
        int totalRegistros,
        int paginaCorrente,
        int tamanhoPagina = Configuracao.PadraoTamanhoPagina) : base(dados)
    {
        Dados = dados;
        TotalRegistros = totalRegistros;
        PaginaCorrente = paginaCorrente;
        TamanhoPagina = tamanhoPagina;
    }

    public PaginacaoResponse(
        TDado? dados,
        int codigo = Configuracao.PadraoCodigoSituacao,
        string? mensagem = null) : base(dados, codigo, mensagem)
    {

    }

    public int PaginaCorrente { get; set; }
    public int TotalPaginas => (int)Math.Ceiling(TotalRegistros / (double)TamanhoPagina);
    public int TamanhoPagina { get; set; } = Configuracao.PadraoTamanhoPagina;
    public int TotalRegistros { get; set; }
}
