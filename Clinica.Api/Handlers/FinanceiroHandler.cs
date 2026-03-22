using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Handlers;

public class FinanceiroHandler(AppDbContext context) : IFinanceiroHandler
{
    public async Task<Response<Financeiro?>> AlterarAsync(AlterarFinanceiroRequest request)
    {
        try
        {
            var financeiro = await context.Financeiros
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (financeiro is null)
                return new Response<Financeiro?>(null, 404, "[FIN002] Financeiro não encontrado!");

            financeiro.ContratoId = request.ContratoId;
            financeiro.DataEmissao = request.DataEmissao;
            financeiro.DataVencimento = request.DataVencimento;
            financeiro.DataPagamento = request.DataPagamento;
            financeiro.DataCancelamento = request.DataCancelamento;
            financeiro.Valor = request.Valor;
            financeiro.ValorMora = request.ValorMora;
            financeiro.ValorJuros = request.ValorJuros;
            financeiro.ValorDesconto = request.ValorDesconto;
            financeiro.ValorPago = request.ValorPago;
            financeiro.Situacao = request.Situacao;
            financeiro.NumeroParcela = request.NumeroParcela;
            financeiro.TipoFinanceiro = request.TipoFinanceiro;
            financeiro.Observacao = request.Observacao;

            context.Financeiros.Update(financeiro);
            await context.SaveChangesAsync();

            return new Response<Financeiro?>(financeiro, mensagem: "Financeiro alterado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Financeiro?>(null, 500, "[FIN003] Falha ao alterar o financeiro! " + ex.Message);
        }
    }

    public async Task<Response<Financeiro?>> CriarAsync(CriarFinanceiroRequest request)
    {
        try
        {
            var financeiro = new Financeiro
            {
                ContratoId = request.ContratoId,
                DataEmissao = request.DataEmissao,
                DataVencimento = request.DataVencimento,
                DataPagamento = request.DataPagamento,
                DataCancelamento = request.DataCancelamento,
                Valor = request.Valor,
                ValorMora = request.ValorMora,
                ValorJuros = request.ValorJuros,
                ValorDesconto = request.ValorDesconto,
                ValorPago = request.ValorPago,
                Situacao = request.Situacao,
                NumeroParcela = request.NumeroParcela,
                TipoFinanceiro = request.TipoFinanceiro,
                Observacao = request.Observacao
            };

            await context.Financeiros.AddAsync(financeiro);
            await context.SaveChangesAsync();

            return new Response<Financeiro?>(financeiro, 201, "Financeiro criado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Financeiro?>(null, 500, "[FIN001] Falha ao criar o financeiro! " + ex.Message);
        }
    }

    public async Task<Response<Financeiro?>> ExcluirAsync(ExcluirFinanceiroRequest request)
    {
        try
        {
            var financeiro = await context.Financeiros
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (financeiro is null)
                return new Response<Financeiro?>(null, 404, "[FIN004] Financeiro não encontrado!");

            context.Financeiros.Remove(financeiro);
            await context.SaveChangesAsync();

            return new Response<Financeiro?>(financeiro, mensagem: "Financeiro excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Financeiro?>(null, 500, "[FIN005] Falha ao excluir o financeiro! " + ex.Message);
        }
    }

    public async Task<Response<Financeiro?>> ListarFinanceiroPorIdAsync(ListarFinanceiroPorIdRequest request)
    {
        try
        {
            var financeiro = await context
                .Financeiros
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return financeiro is null
                ? new Response<Financeiro?>(null, 404, "[FIN006] Financeiro não encontrado!")
                : new Response<Financeiro?>(financeiro);
        }
        catch (Exception ex)
        {
            return new Response<Financeiro?>(null, 500, "[FIN007] Falha ao listar o financeiro! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Financeiro>?>> ListarFinanceirosPorContratoIdAsync(ListarFinanceirosPorContratoIdRequest request)
    {
        try
        {
            var consulta = context
                .Financeiros
                .AsNoTracking()
                .Where(x => x.ContratoId == request.ContratoId)
                .OrderBy(x => x.DataEmissao);

            var financeiros = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Financeiro>?>(
                    financeiros,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Financeiro>?>(null, 500, "[FIN008] Falha ao listar o(s) financeiro(s) por contrato! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Financeiro>?>> ListarTodosFinanceirosAsync(ListarTodosFinanceirosRequest request)
    {
        try
        {
            var consulta = context
            .Financeiros
            .AsNoTracking()
            .OrderBy(x => x.ContratoId)
            .OrderBy(x => x.DataEmissao);

            var financeiros = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Financeiro>?>(
                    financeiros,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Financeiro>?>(null, 500, "[FIN010] Falha ao listar o(s) financeiro(s)! " + ex.Message);
        }
    }
}
