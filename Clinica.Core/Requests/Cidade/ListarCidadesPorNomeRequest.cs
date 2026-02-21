namespace Clinica.Core.Requests.Cidade;

public class ListarCidadesPorNomeRequest : PaginacaoRequest
{
    public string Nome { get; set; } = string.Empty;
}
