using Clinica.Api.Data;
using Clinica.Core.Common.Extensions;
using Clinica.Core.Enums;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Handlers;

public class ContratoHandler(AppDbContext context) : IContratoHandler
{
    public async Task<Response<Contrato?>> AlterarAsync(AlterarContratoRequest request)
    {
        try
        {
            var contrato = await context.Contratos
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (contrato is null)
                return new Response<Contrato?>(null, 404, "[CON002] Contrato não encontrado!");

            contrato.PacienteId = request.PacienteId;
            contrato.ResponsavelId = request.ResponsavelId;
            contrato.VinculoId = request.VinculoId;
            contrato.Situacao = request.Situacao;
            contrato.DataEmissao = request.DataEmissao;
            contrato.DataInicio = request.DataInicio;
            contrato.DataTermino = request.DataTermino;
            contrato.DataCancelamento = request.DataCancelamento;
            contrato.Periodo = request.Periodo;
            contrato.ValorContrato = request.ValorContrato;
            contrato.ValorDesconto = request.ValorDesconto;
            contrato.ValorContratoLiquido = request.ValorContratoLiquido;
            contrato.NumeroParcela = request.NumeroParcela;
            contrato.ValorEntrada = request.ValorEntrada;
            contrato.ValorParcela = request.ValorParcela;
            contrato.DataEntrada = request.DataEntrada;
            contrato.DiaVencimentoDemaisParcelas = request.DiaVencimentoDemaisParcelas;
            contrato.ValorProfissionalEquipe = request.ValorProfissionalEquipe;
            contrato.ValorProfissionalEquipe_Hora = request.ValorProfissionalEquipe_Hora;
            contrato.ValorTerapeutico = request.ValorTerapeutico;
            contrato.Observacao = request.Observacao;
            contrato.ValorCreditoMensal = request.ValorCreditoMensal;

            context.Contratos.Update(contrato);
            await context.SaveChangesAsync();

            return new Response<Contrato?>(contrato, mensagem: "Contrato alterado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Contrato?>(null, 500, "[CON003] Falha ao alterar o contrato! " + ex.Message);
        }
    }

    public async Task<Response<Contrato?>> CriarAsync(CriarContratoRequest request)
    {
        var financeiroHandler = new FinanceiroHandler(context);
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var contrato = new Contrato
            {
                PacienteId = request.PacienteId,
                ResponsavelId = request.ResponsavelId,
                VinculoId = request.VinculoId,
                Situacao = request.Situacao,
                DataEmissao = request.DataEmissao,
                DataInicio = request.DataInicio,
                DataTermino = request.DataTermino,
                DataCancelamento = request.DataCancelamento,
                Periodo = request.Periodo,
                ValorContrato = request.ValorContrato,
                ValorDesconto = request.ValorDesconto,
                ValorContratoLiquido = request.ValorContratoLiquido,
                NumeroParcela = request.NumeroParcela,
                ValorEntrada = request.ValorEntrada,
                ValorParcela = request.ValorParcela,
                DataEntrada = request.DataEntrada,
                DiaVencimentoDemaisParcelas = request.DiaVencimentoDemaisParcelas,
                ValorProfissionalEquipe = request.ValorProfissionalEquipe,
                ValorProfissionalEquipe_Hora = request.ValorProfissionalEquipe_Hora,
                ValorTerapeutico = request.ValorTerapeutico,
                Observacao = request.Observacao,
                ValorCreditoMensal = request.ValorCreditoMensal
            };

            await context.Contratos.AddAsync(contrato);
            await context.SaveChangesAsync();

            var requestContrato = new ListarContratoPorIdRequest { Id = contrato.Id };

            await financeiroHandler.GerarFinanceiroAsync(requestContrato);

            await financeiroHandler.GerarCreditoAsync(requestContrato);

            await transaction.CommitAsync();

            var listarContratoPorIdRequest = new ListarContratoPorIdRequest { Id = contrato?.Id ?? 0 };
            var retorno = await ListarContratoPorIdAsync(listarContratoPorIdRequest);

            return new Response<Contrato?>(retorno.Dados, 201, "Contrato criado com sucesso!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();

            return new Response<Contrato?>(null, 500, "[CON001] Falha ao criar o contrato! " + ex.Message);
        }
    }

    public async Task<Response<Contrato?>> ExcluirAsync(ExcluirContratoRequest request)
    {
        try
        {
            var contrato = await context.Contratos
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (contrato is null)
                return new Response<Contrato?>(null, 404, "[CON004] Contrato não encontrado!");

            context.Contratos.Remove(contrato);
            await context.SaveChangesAsync();

            return new Response<Contrato?>(contrato, mensagem: "Contrato excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Contrato?>(null, 500, "[CON005] Falha ao excluir o contrato! " + ex.Message);
        }
    }

    public async Task<Response<Contrato?>> ListarContratoPorIdAsync(ListarContratoPorIdRequest request)
    {
        try
        {
            var contrato = await 
                (from c in context.Contratos
                 join p in context.Pacientes on c.PacienteId equals p.Id
                 join r in context.Responsaveis on c.ResponsavelId equals r.Id
                 join v in context.Vinculos on c.VinculoId equals v.Id
                 where c.Id == request.Id
                 select new Contrato
                 {
                     Id = c.Id,
                     PacienteId = c.PacienteId,
                     ResponsavelId = c.ResponsavelId,
                     VinculoId = c.VinculoId,
                     Situacao = c.Situacao,
                     DataEmissao = c.DataEmissao,
                     DataInicio = c.DataInicio,
                     DataTermino = c.DataTermino,
                     DataCancelamento = c.DataCancelamento,
                     Periodo = c.Periodo,
                     ValorContrato = c.ValorContrato,
                     ValorDesconto = c.ValorDesconto,
                     ValorContratoLiquido = c.ValorContratoLiquido,
                     NumeroParcela = c.NumeroParcela,
                     ValorEntrada = c.ValorEntrada,
                     ValorParcela = c.ValorParcela,
                     DataEntrada = c.DataEntrada,
                     DiaVencimentoDemaisParcelas = c.DiaVencimentoDemaisParcelas,
                     ValorProfissionalEquipe = c.ValorProfissionalEquipe,
                     ValorProfissionalEquipe_Hora = c.ValorProfissionalEquipe_Hora,
                     ValorTerapeutico = c.ValorTerapeutico,
                     Observacao = c.Observacao,
                     ValorCreditoMensal = c.ValorCreditoMensal,
                     Paciente = p,
                     Responsavel = r,
                     Vinculo = v,
                 })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return contrato is null
                ? new Response<Contrato?>(null, 404, "[CON006] Contrato não encontrado!")
                : new Response<Contrato?>(contrato);
        }
        catch (Exception ex)
        {
            return new Response<Contrato?>(null, 500, "[CON007] Falha ao listar o contrato! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Contrato>?>> ListarTodosContratosAsync(ListarTodosContratosRequest request)
    {
        try
        {
            var consulta = 
                (from c in context.Contratos
                 join p in context.Pacientes on c.PacienteId equals p.Id
                 join r in context.Responsaveis on c.ResponsavelId equals r.Id
                 join v in context.Vinculos on c.VinculoId equals v.Id                 
                 select new Contrato
                 {
                     Id = c.Id,
                     PacienteId = c.PacienteId,
                     ResponsavelId = c.ResponsavelId,
                     VinculoId = c.VinculoId,
                     Situacao = c.Situacao,
                     DataEmissao = c.DataEmissao,
                     DataInicio = c.DataInicio,
                     DataTermino = c.DataTermino,
                     DataCancelamento = c.DataCancelamento,
                     Periodo = c.Periodo,
                     ValorContrato = c.ValorContrato,
                     ValorDesconto = c.ValorDesconto,
                     ValorContratoLiquido = c.ValorContratoLiquido,
                     NumeroParcela = c.NumeroParcela,
                     ValorEntrada = c.ValorEntrada,
                     ValorParcela = c.ValorParcela,
                     DataEntrada = c.DataEntrada,
                     DiaVencimentoDemaisParcelas = c.DiaVencimentoDemaisParcelas,
                     ValorProfissionalEquipe = c.ValorProfissionalEquipe,
                     ValorProfissionalEquipe_Hora = c.ValorProfissionalEquipe_Hora,
                     ValorTerapeutico = c.ValorTerapeutico,
                     Observacao = c.Observacao,
                     ValorCreditoMensal = c.ValorCreditoMensal,
                     Paciente = p,
                     Responsavel = r,
                     Vinculo = v,
                 })
                .AsNoTracking()
                .OrderBy(x => x.DataEmissao);

            var contratos = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Contrato>?>(
                contratos,
                quantidade,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Contrato>?>(null, 500, "[CON008] Falha ao listar o(s) contrato(s)! " + ex.Message);
        }
    }
}
