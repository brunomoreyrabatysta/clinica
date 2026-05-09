using Clinica.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Vinculos;

public class CriarVinculoRequest : BaseRequest
{

    [Required(ErrorMessage = "O nome não foi preenchido!")]
    public string Nome { get; set; } = string.Empty;

    public ESituacao Situacao { get; set; }
}
