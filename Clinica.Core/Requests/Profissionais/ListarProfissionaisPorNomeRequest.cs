namespace Clinica.Core.Requests.Profissionais;

public class ListarProfissionaisPorNomeRequest : PaginacaoRequest
{
    public string Nome { get; set; } = string.Empty;
}
