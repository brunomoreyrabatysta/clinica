using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface IFinanceiroHandler
{
    Task<Response<Financeiro?>> CriarAsync(CriarFinanceiroRequest request);
    Task<Response<Financeiro?>> AlterarAsync(AlterarFinanceiroRequest request);
    Task<Response<Financeiro?>> ExcluirAsync(ExcluirFinanceiroRequest request);
    Task<Response<Financeiro?>> ListarFinanceiroPorIdAsync(ListarFinanceiroPorIdRequest request);
    Task<PaginacaoResponse<List<Financeiro>?>> ListarFinanceirosPorContratoIdAsync(ListarFinanceirosPorContratoIdRequest request);
    Task<PaginacaoResponse<List<Financeiro>?>> ListarTodosFinanceirosAsync(ListarTodosFinanceirosRequest request);
}
