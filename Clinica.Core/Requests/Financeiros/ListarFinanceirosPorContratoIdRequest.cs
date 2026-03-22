namespace Clinica.Core.Requests.Financeiros;

public class ListarFinanceirosPorContratoIdRequest : PaginacaoRequest
{
    public long ContratoId { get; set; }
}
