using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.UnidadesFederativas;

public class CriarUnidadeFederativaRequest : BaseRequest
{
    [Required(ErrorMessage = "O nome não foi preenchido!")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A sigla não foi preenchida!")]
    public string Sigla { get; set; } = string.Empty;
}
