using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.UnidadeFederativa;

public class AlterarUnidadeFederativaRequest : BaseRequest
{
    [Required(ErrorMessage = "O código da unidade federativa não foi preenchido!")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O nome não foi preenchido!")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A sigla não foi preenchida!")]
    public string Sigla { get; set; } = string.Empty;
}
