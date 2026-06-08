using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Responsaveis;

public class ExcluirResponsavelRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do responsável não foi preenchido!")]
    public long Id { get; set; }
}
