using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Cidades;

public class ExcluirCidadeRequest : BaseRequest
{
    [Required(ErrorMessage = "O código da cidade não foi preenchido!")]
    public long Id { get; set; }
}
