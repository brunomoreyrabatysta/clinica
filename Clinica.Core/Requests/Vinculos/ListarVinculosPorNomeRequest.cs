namespace Clinica.Core.Requests.Vinculos;

public class ListarVinculosPorNomeRequest : PaginacaoRequest
{
    public string Nome { get; set; } = string.Empty;
}
