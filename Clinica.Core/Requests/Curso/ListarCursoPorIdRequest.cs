namespace Clinica.Core.Requests.Curso;

public class ListarCursoPorIdRequest : PaginacaoRequest
{
    public long Id { get; set; }
}
