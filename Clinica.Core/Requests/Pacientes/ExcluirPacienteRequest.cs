using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Pacientes;

public class ExcluirPacienteRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do paciente não foi preenchido!")]
    public long Id { get; set; }
}
