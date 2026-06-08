using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Vinculos;

public class ExcluirVinculoRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do vínculo não foi preenchido!")]
    public int Id { get; set; }
}
