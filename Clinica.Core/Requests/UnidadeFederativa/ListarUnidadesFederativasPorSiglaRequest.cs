namespace Clinica.Core.Requests.UnidadeFederativa;

public class ListarUnidadesFederativasPorSiglaRequest : PaginacaoRequest
{
    public string Sigla { get; set; } = string.Empty;
}
