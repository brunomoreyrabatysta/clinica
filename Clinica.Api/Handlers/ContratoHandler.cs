using Clinica.Api.Data;
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
            contrato.NumeroParcela = request.NumeroParcela;
            contrato.ValorEntrada = request.ValorEntrada;
            contrato.ValorParcela = request.ValorParcela;
            contrato.DataEntrada = request.DataEntrada;
            contrato.DiaVencimentoDemaisParcelas = request.DiaVencimentoDemaisParcelas;
            contrato.ValorProfissionalEquipe = request.ValorProfissionalEquipe;
            contrato.ValorProfissionalEquipe_Hora = request.ValorProfissionalEquipe_Hora;
            contrato.ValorTerapeutico = request.ValorTerapeutico;

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
                NumeroParcela = request.NumeroParcela,
                ValorEntrada = request.ValorEntrada,
                ValorParcela = request.ValorParcela,
                DataEntrada = request.DataEntrada,
                DiaVencimentoDemaisParcelas = request.DiaVencimentoDemaisParcelas,
                ValorProfissionalEquipe = request.ValorProfissionalEquipe,
                ValorProfissionalEquipe_Hora = request.ValorProfissionalEquipe_Hora,
                ValorTerapeutico = request.ValorTerapeutico
            };

            await context.Contratos.AddAsync(contrato);
            await context.SaveChangesAsync();

            return new Response<Contrato?>(contrato, 201, "Contrato criado com sucesso!");
        }
        catch (Exception ex)
        {
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
            var contrato = await context
                .Contratos
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
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
            var consulta = context
                .Contratos
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
