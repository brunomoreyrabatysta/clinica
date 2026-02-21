using Microsoft.EntityFrameworkCore;
using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;
using Clinica.Core.Responses;

namespace Clinica.Api.Handlers;

public class TipoCursoHandler(AppDbContext context) : ITipoCursoHandler
{
    public async Task<Response<TipoCurso?>> CriarAsync(CriarTipoCursoRequest request)
    {
        try
        {
            var tipoCurso = new TipoCurso
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao
            };

            await context.TiposCurso.AddAsync(tipoCurso);
            await context.SaveChangesAsync();

            return new Response<TipoCurso?>(tipoCurso, 201, "Tipo de curso criado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<TipoCurso?>(null, 500, "[FTC001] Falha ao criar o tipo de curso! " + ex.Message);
        }
    }

    public async Task<Response<TipoCurso?>> AlterarAsync(AlterarTipoCursoRequest request)
    {
        try
        {
            var tipoCurso = await context.TiposCurso.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (tipoCurso is null)
                return new Response<TipoCurso?>(null, 404, "[FTC002] Tipo de curso não encontrado!");

            tipoCurso.Titulo = request.Titulo;
            tipoCurso.Descricao = request.Descricao;

            context.TiposCurso.Update(tipoCurso);
            await context.SaveChangesAsync();

            return new Response<TipoCurso?>(tipoCurso, mensagem: "Tipo de curso alterado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<TipoCurso?>(null, 500, "[FTC003] Falha ao alterar o tipo de curso! " + ex.Message);
        }
    }

    public async Task<Response<TipoCurso?>> ExcluirAsync(ExcluirTipoCursoRequest request)
    {
        try
        {
            var tipoCurso = await context.TiposCurso.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (tipoCurso is null)
                return new Response<TipoCurso?>(null, 404, "[FTC004] Tipo de curso não encontrado!");

            context.TiposCurso.Remove(tipoCurso);
            await context.SaveChangesAsync();

            return new Response<TipoCurso?>(tipoCurso, mensagem: "Tipo de curso excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<TipoCurso?>(null, 500, "[FTC005] Falha ao excluir o tipo de curso! " + ex.Message);
        }
    }

    public async Task<Response<TipoCurso?>> ListarPorIdAsync(ListarTipoCursoPorIdRequest request)
    {
        try
        {
            var tipoCurso = await context
                .TiposCurso
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return tipoCurso is null
                ? new Response<TipoCurso?>(null, 404, "[FTC006] Tipo de curso não encontrado!")
                : new Response<TipoCurso?>(tipoCurso);
        }
        catch (Exception ex)
        {
            return new Response<TipoCurso?>(null, 500, "[FTC007] Falha ao listar o tipo de curso! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<TipoCurso>>> ListarTodosAsync(ListarTodosTipoCursoRequest request)
    {
        try
        {
            var consulta = context
                .TiposCurso
                .AsNoTracking()
                .OrderBy(x => x.Titulo);

            var tiposCurso = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<TipoCurso>>(
                tiposCurso,
                quantidade,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<TipoCurso>>(null, 500, "[FTC008] Falha ao listar os tipos de curso! " + ex.Message);
        }

    }
}
