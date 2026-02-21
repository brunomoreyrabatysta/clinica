namespace Clinica.Core.Requests.Curso;

public class ListarCursoPorPeriodoRequest : PaginacaoRequest
{
    public DateTime? DataInicio { get; set; }

    public DateTime? DataTermino { get; set; }
}
