using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadesFederativas;
using Clinica.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Handlers;

public class UnidadeFederativaHandler(AppDbContext context) : IUnidadeFederativaHandler
{
    public async Task<Response<UnidadeFederativa?>> AlterarAsync(AlterarUnidadeFederativaRequest request)
    {
        try
        {
            var unidadeFederativa = await context.UnidadesFederativa
                                                .Where(x => x.Id == request.Id)
                                                .FirstOrDefaultAsync();

            if (unidadeFederativa is null)
                return new Response<UnidadeFederativa?>(null, 404, "[UF002] Unidade federativa não encontrada!");

            unidadeFederativa.Nome = request.Nome;
            unidadeFederativa.Sigla = request.Sigla;

            context.UnidadesFederativa.Update(unidadeFederativa);
            await context.SaveChangesAsync();

            return new Response<UnidadeFederativa?>(unidadeFederativa, mensagem: "Unidade federativa alterada com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<UnidadeFederativa?>(null, 500, "[UF003] Falha ao alterar a unidade federativa! " + ex.Message);
        }
    }

    public async Task<Response<UnidadeFederativa?>> CriarAsync(CriarUnidadeFederativaRequest request)
    {
        try
        {
            var unidadeFederativa = new UnidadeFederativa
            {
                Nome = request.Nome,
                Sigla = request.Sigla
            };

            await context.UnidadesFederativa.AddAsync(unidadeFederativa);
            await context.SaveChangesAsync();

            return new Response<UnidadeFederativa?>(unidadeFederativa, 201, "Unidade federativa criada com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<UnidadeFederativa?>(null, 500, "[UF001] Falha ao criar a unidade federativa! " + ex.Message);
        }
    }

    public async Task<Response<UnidadeFederativa?>> ExcluirAsync(ExcluirUnidadeFederativaRequest request)
    {
        try
        {
            var unidadeFederativa = await context.UnidadesFederativa
                                                .Where(x => x.Id == request.Id)
                                                .FirstOrDefaultAsync();

            if (unidadeFederativa is null)
                return new Response<UnidadeFederativa?>(null, 404, "[UF004] Unidade federativa não encontrada!");

            context.UnidadesFederativa.Remove(unidadeFederativa);
            await context.SaveChangesAsync();

            return new Response<UnidadeFederativa?>(unidadeFederativa, mensagem: "Unidade federativa excluída com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<UnidadeFederativa?>(null, 500, "[UF05] Falha ao excluir a unidade federativa! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<UnidadeFederativa>?>> ListarTodasUnidadesFederativasAsync(ListarTodasUnidadesFederativasRequest request)
    {
        try
        {
            var consulta = context
                .UnidadesFederativa
                .AsNoTracking()
                .OrderBy(x => x.Sigla);

            var unidadesFederativa = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<UnidadeFederativa>?>(
                unidadesFederativa,
                quantidade,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<UnidadeFederativa>?>(null, 500, "[UF008] Falha ao listar a(s) unidade(s) federativa(s)! " + ex.Message);
        }
    }

    public async Task<Response<UnidadeFederativa?>> ListarUnidadeFederativaPorIdAsync(ListarUnidadeFederativaPorIdRequest request)
    {
        try
        {
            var unidadeFederativa = await context
                .UnidadesFederativa
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return unidadeFederativa is null
                ? new Response<UnidadeFederativa?>(null, 404, "[UF006] Unidade federativa não encontrada!")
                : new Response<UnidadeFederativa?>(unidadeFederativa);
        }
        catch (Exception ex)
        {
            return new Response<UnidadeFederativa?>(null, 500, "[UF007] Falha ao listar a unidade federativa! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<UnidadeFederativa>?>> ListarUnidadesFederativasPorNomeAsync(ListarUnidadesFederativasPorNomeRequest request)
    {
        try
        {
            var consulta = context
            .UnidadesFederativa
            .AsNoTracking()
            .Where(x => x.Nome.Contains(request.Nome))
            .OrderBy(x => x.Sigla);

            var unidadesFederativa = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<UnidadeFederativa>?>(
                    unidadesFederativa,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<UnidadeFederativa>?>(null, 500, "[UF010] Falha ao listar a(s) unidade(s) federativa(s) por nome! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<UnidadeFederativa>?>> ListarUnidadesFederativasPorSiglaAsync(ListarUnidadesFederativasPorSiglaRequest request)
    {
        try
        {
            var consulta = context
            .UnidadesFederativa
            .AsNoTracking()
            .Where(x => x.Sigla.Contains(request.Sigla))
            .OrderBy(x => x.Sigla);

            var unidadesFederativa = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<UnidadeFederativa>?>(
                    unidadesFederativa,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<UnidadeFederativa>?>(null, 500, "[UF009] Falha ao listar a(s) unidade(s) federativa(s) por sigla! " + ex.Message);
        }
    }
}
