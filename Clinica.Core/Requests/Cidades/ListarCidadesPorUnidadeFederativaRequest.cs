using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Cidades;

public class ListarCidadesPorUnidadeFederativaRequest : PaginacaoRequest
{
    [Required(ErrorMessage = "O código da unidade federativa não foi preenchido!")]
    public long UnidadeFederativaId { get; set; }
}
