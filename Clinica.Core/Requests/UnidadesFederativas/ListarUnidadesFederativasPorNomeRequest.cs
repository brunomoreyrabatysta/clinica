namespace Clinica.Core.Requests.UnidadesFederativas;

public class ListarUnidadesFederativasPorNomeRequest : PaginacaoRequest
{
    public string Nome { get; set; } = string.Empty;
}
