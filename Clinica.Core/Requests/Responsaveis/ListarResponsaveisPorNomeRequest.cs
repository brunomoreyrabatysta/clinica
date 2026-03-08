namespace Clinica.Core.Requests.Responsaveis;

public class ListarResponsaveisPorNomeRequest : PaginacaoRequest
{
    public string Nome { get; set; } = string.Empty;
}
