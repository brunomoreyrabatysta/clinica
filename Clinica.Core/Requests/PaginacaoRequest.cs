namespace Clinica.Core.Requests;

public abstract class PaginacaoRequest : BaseRequest
{
    public int NumeroPagina { get; set; } = Configuracao.PadraoNumeroPagina;
    public int TamanhoPagina { get; set; } = Configuracao.PadraoTamanhoPagina;
}
