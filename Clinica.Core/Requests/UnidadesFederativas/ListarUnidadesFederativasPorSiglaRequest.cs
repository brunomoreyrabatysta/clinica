namespace Clinica.Core.Requests.UnidadesFederativas;

public class ListarUnidadesFederativasPorSiglaRequest : PaginacaoRequest
{
    public string Sigla { get; set; } = string.Empty;
}
