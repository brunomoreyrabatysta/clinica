namespace Clinica.Core.Requests.Pacientes;

public class ListarPacientesPorNomeRequest : PaginacaoRequest
{
    public string Nome { get; set; } = string.Empty;
}
