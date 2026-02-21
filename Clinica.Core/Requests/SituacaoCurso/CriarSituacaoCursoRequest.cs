using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.SituacaoCurso;

public class CriarSituacaoCursoRequest : BaseRequest
{
    [Required(ErrorMessage = "Título da situação de curso inválido!")]
    [MaxLength(80, ErrorMessage = "O título da situação de curso deve conter no máximo 80 (oitenta) caracteres!")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição da situação de curso inválida!")]
    public string Descricao { get; set; } = string.Empty;
}
