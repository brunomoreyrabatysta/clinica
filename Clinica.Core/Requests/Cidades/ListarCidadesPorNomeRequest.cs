namespace Clinica.Core.Requests.Cidades;

public class ListarCidadesPorNomeRequest : PaginacaoRequest
{
    public string Nome { get; set; } = string.Empty;
}
