using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Financeiros;

public class ExcluirFinanceiroRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do financeiro não foi preenchido!")]
    public long Id { get; set; }
}
