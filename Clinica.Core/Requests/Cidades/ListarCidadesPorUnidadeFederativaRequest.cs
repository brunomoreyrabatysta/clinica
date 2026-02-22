namespace Clinica.Core.Requests.Cidades;

public class ListarCidadesPorUnidadeFederativaRequest : PaginacaoRequest
{
    public long UnidadeFederativaId { get; set; }
}
