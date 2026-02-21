using Microsoft.EntityFrameworkCore;
using Clinica.Api.Data;
using Clinica.Core.Common.Extensions;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Curso;
using Clinica.Core.Responses;

namespace Clinica.Api.Handlers;

public class CursoHandler(AppDbContext context) : ICursoHandler
{
    public async Task<Response<Curso?>> AlterarAsync(AlterarCursoRequest request)
    {
        try
        {
            var curso = await context
            .Cursos
            .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (curso is null)
                return new Response<Curso?>(null, 404, "[FCS002] Curso não encontrado!");

            curso.Titulo = request.Titulo;
            curso.Descricao = request.Descricao;
            curso.CodigoCredencial = request.CodigoCredencial;
            curso.OrganizacaoEmissora = request.OrganizacaoEmissora;
            curso.DataEmissao = request.DataEmissao;
            curso.DataInicio = request.DataInicio;
            curso.DataTermino = request.DataTermino;
            curso.DataExpiracao = request.DataExpiracao;
            curso.UrlCredencial = request.UrlCredencial;
            curso.Competencia = request.Competencia;
            curso.CargaHoraria = request.CargaHoraria;
            curso.TipoCursoId = request.TipoCursoId;
            curso.SituacaoCursoId = request.SituacaoCursoId;

            context.Cursos.Update(curso);
            await context.SaveChangesAsync();

            return new Response<Curso?>(curso, mensagem: "Curso alterado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Curso?>(null, 500, "[FCS003] Falha ao alterar o curso! " + ex.Message);
        }
    }

    public async Task<Response<Curso?>> CriarAsync(CriarCursoRequest request)
    {
        try
        {
            var curso = new Curso
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                CodigoCredencial = request.CodigoCredencial,
                OrganizacaoEmissora = request.OrganizacaoEmissora,
                DataEmissao = request.DataEmissao,
                DataInicio = request.DataInicio,
                DataTermino = request.DataTermino,
                DataExpiracao = request.DataExpiracao,
                UrlCredencial = request.UrlCredencial,
                Competencia = request.Competencia,
                CargaHoraria = request.CargaHoraria,
                TipoCursoId = request.TipoCursoId,
                SituacaoCursoId = request.SituacaoCursoId
            };

            await context.Cursos.AddAsync(curso);
            await context.SaveChangesAsync();

            return new Response<Curso?>(curso, 201, "Curso criado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Curso?>(null, 500, "[FCS001] Falha ao criar o curso! " + ex.Message);
        }
    }

    public async Task<Response<Curso?>> ExcluirAsync(ExcluirCursoRequest request)
    {
        try
        {
            var curso = await context.Cursos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (curso is null)
                return new Response<Curso?>(null, 404, "[FCS004] Curso não encontrado!");

            context.Cursos.Remove(curso);
            await context.SaveChangesAsync();

            return new Response<Curso?>(curso, mensagem: "Curso excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Curso?>(null, 500, "[FCS005] Falha ao excluir o curso! " + ex.Message);
        }
    }

    public async Task<Response<Curso?>> ListarCursoPorIdAsync(ListarCursoPorIdRequest request)
    {
        try
        {
            var curso = await context
                .Cursos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return curso is null
                ? new Response<Curso?>(null, 404, "[FCS006] Curso não encontrado!")
                : new Response<Curso?>(curso);
        }
        catch (Exception ex)
        {
            return new Response<Curso?>(null, 500, "[FCS007] Falha ao listar o curso! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Curso>?>> ListarCursoPorPeriodoAsync(ListarCursoPorPeriodoRequest request)
    {
        try
        {
            request.DataInicio ??= DateTime.Now.ObterPrimeiroDia();
            request.DataTermino ??= DateTime.Now.ObterUltimoDia();
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Curso>?>(null, 500, "[FCS007] Falha ao listar o(s) curso(s)! " + ex.Message);
        }

        try
        {
            var consulta = context
            .Cursos
            .AsNoTracking()
            .Where(x => x.DataEmissao >= request.DataInicio && x.DataEmissao <= request.DataTermino)
            .OrderBy(x => x.DataEmissao);

            var cursos = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Curso>?>(
                    cursos,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Curso>?>(null, 500, "[FCS007] Falha ao listar o(s) curso(s)! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Curso>?>> ListarCursoPorTipoCursoAsync(ListarCursoPorTipoCursoRequest request)
    {
        try
        {
            var consulta = context
            .Cursos
            .AsNoTracking()
            .Where(x => x.TipoCursoId == request.TipoCursoId)
            .OrderBy(x => x.DataEmissao);

            var cursos = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Curso>?>(
                    cursos,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Curso>?>(null, 500, "[FCS008] Falha ao listar o(s) curso(s) pelo tipo de curso! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Curso>?>> ListarTodosCursoAsync(ListarTodosCursoRequest request)
    {
        try
        {
            var consulta = context
            .Cursos
            .AsNoTracking()
            .OrderBy(x => x.DataEmissao);

            var cursos = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Curso>?>(
                    cursos,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Curso>?>(null, 500, "[FCS008] Falha ao listar o(s) curso(s)! " + ex.Message);
        }
    }
}
