namespace Clinica.Core.Requests.Paciente;

public class ListarPacientesPorNomeRequest : PaginacaoRequest
{
    public string Nome { get; set; } = string.Empty;
}
