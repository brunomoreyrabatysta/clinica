using Microsoft.EntityFrameworkCore;
using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.SituacaoCurso;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;

namespace Clinica.Api.Handlers;

public class SituacaoCursoHandler(AppDbContext context) : ISituacaoCursoHandler
{
    public async Task<Response<SituacaoCurso?>> CriarAsync(CriarSituacaoCursoRequest request)
    {
        try
        {
            var situacaoCurso = new SituacaoCurso
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao
            };

            await context.SituacoesCurso.AddAsync(situacaoCurso);
            await context.SaveChangesAsync();

            return new Response<SituacaoCurso?>(situacaoCurso, 201, "Situação de curso criado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<SituacaoCurso?>(null, 500, "[FSC001] Falha ao criar a situação de curso! " + ex.Message);
        }
    }

    public async Task<Response<SituacaoCurso?>> AlterarAsync(AlterarSituacaoCursoRequest request)
    {
        try
        {
            var situacaoCurso = await context.SituacoesCurso.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (situacaoCurso is null)
                return new Response<SituacaoCurso?>(null, 404, "[FSC002] Situação de curso não encontrada!");

            situacaoCurso.Titulo = request.Titulo;
            situacaoCurso.Descricao = request.Descricao;

            context.SituacoesCurso.Update(situacaoCurso);
            await context.SaveChangesAsync();

            return new Response<SituacaoCurso?>(situacaoCurso, mensagem: "Situação de curso alterada com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<SituacaoCurso?>(null, 500, "[FSC003] Falha ao alterar a situação de curso! " + ex.Message);
        }
    }

    public async Task<Response<SituacaoCurso?>> ExcluirAsync(ExcluirSituacaoCursoRequest request)
    {
        try
        {
            var situacaoCurso = await context.SituacoesCurso.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (situacaoCurso is null)
                return new Response<SituacaoCurso?>(null, 404, "[FSC004] Situação de curso não encontrada!");

            context.SituacoesCurso.Remove(situacaoCurso);
            await context.SaveChangesAsync();

            return new Response<SituacaoCurso?>(situacaoCurso, mensagem: "Situação de curso excluída com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<SituacaoCurso?>(null, 500, "[FSC005] Falha ao excluir a situação de curso! " + ex.Message);
        }
    }

    public async Task<Response<SituacaoCurso?>> ListarPorIdAsync(ListarSituacaoCursoPorIdRequest request)
    {
        try
        {
            var situacaoCurso = await context
                .SituacoesCurso
                .Where(x => x.Id == request.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return situacaoCurso is null
                ? new Response<SituacaoCurso?>(null, 404, "[FSC006] Situação de curso não encontrada!")
                : new Response<SituacaoCurso?>(situacaoCurso);
        }
        catch (Exception ex)
        {
            return new Response<SituacaoCurso?>(null, 500, "[FSC007] Falha ao listar o tipo de curso! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<SituacaoCurso>>> ListarTodasAsync(ListarTodasSituacaoCursoRequest request)
    {
        try
        {
            var consulta = context
                .SituacoesCurso
                .AsNoTracking()
                .OrderBy(x => x.Titulo);

            var situacoesCurso = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<SituacaoCurso>>(
                situacoesCurso,
                quantidade,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<SituacaoCurso>>(null, 500, "[FSC008] Falha ao listar as situações do curso! " + ex.Message);
        }

    }
}
