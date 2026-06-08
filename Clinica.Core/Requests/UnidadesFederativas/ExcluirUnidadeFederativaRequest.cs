using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.UnidadesFederativas;

public class ExcluirUnidadeFederativaRequest : BaseRequest
{
    [Required(ErrorMessage = "O código da unidade federativa não foi preenchido!")]
    public long Id { get; set; }
}
