using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.TiposCurso;

public class AlterarTipoCursoRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do tipo de curso está inválido!")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Título do tipo de curso inválido!")]
    [MaxLength(80, ErrorMessage = "O título do tipo de curso deve conter no máximo 80 (oitenta) caracteres!")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição do tipo de curso inválida!")]
    public string? Descricao { get; set; }
}

