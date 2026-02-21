namespace Clinica.Core.Requests.UnidadeFederativa;

public class ListarUnidadesFederativasPorNomeRequest : PaginacaoRequest
{
    public string Nome { get; set; } = string.Empty;
}
