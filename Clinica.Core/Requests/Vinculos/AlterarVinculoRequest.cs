using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Vinculos;

public class AlterarVinculoRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do vínculo não foi preenchido!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome não foi preenchido!")]
    public string Nome { get; set; } = string.Empty;
}
