using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Profissionais;

public class ListarProfissionalPorIdRequest :BaseRequest
{
    [Required(ErrorMessage = "O código do profissional não foi preenchido!")]
    public long Id { get; set; }
}
