using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Prontuarios;
using Clinica.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Handlers;

public class ProntuarioHandler(AppDbContext context) : IProntuarioHandler
{
    public async Task<Response<Prontuario?>> AlterarAsync(AlterarProntuarioRequest request)
    {
        try
        {
            var prontuario = await context.Prontuarios
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (prontuario is null)
                return new Response<Prontuario?>(null, 404, "[PRON002] Prontuário não encontrado!");

            prontuario.DataProntuario = request.DataProntuario;
            prontuario.Cobranca = request.Cobranca;
            prontuario.ProfissionalId = request.ProfissionalId;
            prontuario.Descricao = request.Descricao;
            prontuario.ContratoId = request.ContratoId;  
            prontuario.TempoAtendimento = request.TempoAtendimento;

            context.Prontuarios.Update(prontuario);
            await context.SaveChangesAsync();

            return new Response<Prontuario?>(prontuario, mensagem: "Prontuário alterado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Prontuario?>(null, 500, "[PRON003] Falha ao alterar o prontuário! " + ex.Message);
        }
    }

    public async Task<Response<Prontuario?>> CriarAsync(CriarProntuarioRequest request)
    {
        try
        {
            var prontuario = new Prontuario
            {
                DataProntuario = request.DataProntuario,
                Cobranca = request.Cobranca,
                ProfissionalId = request.ProfissionalId,
                Descricao = request.Descricao,
                ContratoId = request.ContratoId,
                TempoAtendimento = request.TempoAtendimento
            };

            await context.Prontuarios.AddAsync(prontuario);
            await context.SaveChangesAsync();

            return new Response<Prontuario?>(prontuario, 201, "Prontuário criado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Prontuario?>(null, 500, "[PRON001] Falha ao criar o prontuário! " + ex.Message);
        }
    }

    public async Task<Response<Prontuario?>> ExcluirAsync(ExcluirProntuarioRequest request)
    {
        try
        {
            var prontuario = await context.Prontuarios
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (prontuario is null)
                return new Response<Prontuario?>(null, 404, "[PRON004] Prontuário não encontrado!");

            context.Prontuarios.Remove(prontuario);
            await context.SaveChangesAsync();

            return new Response<Prontuario?>(prontuario, mensagem: "Prontuário excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Prontuario?>(null, 500, "[PRON005] Falha ao excluir o prontuário! " + ex.Message);
        }
    }

    public async Task<Response<Prontuario?>> ListarProntuarioPorIdAsync(ListarProntuarioPorIdRequest request)
    {
        try
        {
            var prontuario = await context
                .Prontuarios
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return prontuario is null
                ? new Response<Prontuario?>(null, 404, "[PRON006] Prontuário não encontrado!")
                : new Response<Prontuario?>(prontuario);
        }
        catch (Exception ex)
        {
            return new Response<Prontuario?>(null, 500, "[PRON007] Falha ao listar o prontuário! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Prontuario>?>> ListarProntuariosPorContratoIdAsync(ListarProntuariosPorContratoIdRequest request)
    {
        try
        {
            var consulta = context
            .Prontuarios
            .AsNoTracking()
            .Where(x => x.ContratoId == request.ContratoId)
            .OrderBy(x => x.DataProntuario);

            var prontuarios = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Prontuario>?>(
                    prontuarios,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Prontuario>?>(null, 500, "[PRON009] Falha ao listar o(s) prontuário(s) por contrato! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Prontuario>?>> ListarTodosProntuariosAsync(ListarTodosProntuariosRequest request)
    {
        try
        {
            var consulta = context
                .Prontuarios
                .AsNoTracking()
                .OrderBy(x => x.ContratoId)
                .ThenBy(x => x.DataProntuario);

            var prontuarios = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Prontuario>?>(
                prontuarios,
                quantidade,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Prontuario>?>(null, 500, "[PRON008] Falha ao listar o(s) prontuário(s)! " + ex.Message);
        }
    }
}
