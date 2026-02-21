using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.SituacaoCurso;

public class ExcluirSituacaoCursoRequest : BaseRequest
{
    [Required(ErrorMessage = "O código da situação de curso está inválido!")]
    public int Id { get; set; }
}
