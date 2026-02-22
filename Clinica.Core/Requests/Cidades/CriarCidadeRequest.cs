using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Cidades;

public class CriarCidadeRequest : BaseRequest
{
    [Required(ErrorMessage = "O nome não foi preenchido!")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A unidade federativa não foi preenchida!")]
    public long UnidadeFederativaId { get; set; }
}
