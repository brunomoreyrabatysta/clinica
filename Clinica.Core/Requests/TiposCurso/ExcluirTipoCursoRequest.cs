using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.TiposCurso;

public class ExcluirTipoCursoRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do tipo de curso está inválido!")]
    public int Id { get; set; }
}
