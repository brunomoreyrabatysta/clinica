using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Prontuarios;

public class ListarProntuariosPorContratoIdRequest : PaginacaoRequest
{
    [Required(ErrorMessage = "O código do contrato não foi preenchido!")]
    public long ContratoId { get; set; }
}
