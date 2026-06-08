using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Prontuarios;

public class ListarProntuarioPorIdRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do prontuário não foi preenchido!")]
    public long Id { get; set; }
}
