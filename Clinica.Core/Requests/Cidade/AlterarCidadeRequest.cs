using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Cidade;

public class AlterarCidadeRequest : BaseRequest
{
    [Required(ErrorMessage = "O código da cidade não foi preenchido!")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O nome não foi preenchido!")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A unidade federativa não foi preenchida!")]
    public int UnidadeFederativaId { get; set; }
}
