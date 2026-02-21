namespace Clinica.Core.Requests.Cidade;

public class ListarCidadesPorUnidadeFederativaRequest : PaginacaoRequest
{
    public int UnidadeFederativaId { get; set; }
}
