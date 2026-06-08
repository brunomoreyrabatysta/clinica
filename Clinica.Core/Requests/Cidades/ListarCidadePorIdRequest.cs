using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Cidades;

public class ListarCidadePorIdRequest : BaseRequest
{
    [Required(ErrorMessage = "O código da cidade não foi preenchido!")]
    public long Id { get; set; }
}
