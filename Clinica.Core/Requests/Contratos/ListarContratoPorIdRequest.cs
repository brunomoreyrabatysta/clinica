using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Contratos;

public class ListarContratoPorIdRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do contrato não foi preenchido!")]
    public long Id { get; set; }
}
