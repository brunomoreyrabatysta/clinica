using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Clinica.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Handlers;

public class CidadeHandler(AppDbContext context) : ICidadeHandler
{
    public async Task<Response<Cidade?>> AlterarAsync(AlterarCidadeRequest request)
    {
        try
        {
            var cidade = await context.Cidades
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (cidade is null)
                return new Response<Cidade?>(null, 404, "[CID002] Cidade não encontrada!");

            cidade.Nome = request.Nome;
            cidade.UnidadeFederativaId= request.UnidadeFederativaId;

            context.Cidades.Update(cidade);
            await context.SaveChangesAsync();

            return new Response<Cidade?>(cidade, mensagem: "Cidade alterada com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Cidade?>(null, 500, "[CID03] Falha ao alterar a cidade! " + ex.Message);
        }
    }

    public async Task<Response<Cidade?>> CriarAsync(CriarCidadeRequest request)
    {
        try
        {
            var cidade = new Cidade
            {
                Nome = request.Nome,
                UnidadeFederativaId = request.UnidadeFederativaId
            };

            await context.Cidades.AddAsync(cidade);
            await context.SaveChangesAsync();

            return new Response<Cidade?>(cidade, 201, "Cidade criada com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Cidade?>(null, 500, "[CID001] Falha ao criar a cidade! " + ex.Message);
        }
    }

    public async Task<Response<Cidade?>> ExcluirAsync(ExcluirCidadeRequest request)
    {
        try
        {
            var cidade = await context.Cidades
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (cidade is null)
                return new Response<Cidade?>(null, 404, "[CID004] Cidade não encontrada!");

            context.Cidades.Remove(cidade);
            await context.SaveChangesAsync();

            return new Response<Cidade?>(cidade, mensagem: "Cidade excluída com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Cidade?>(null, 500, "[CID05] Falha ao excluir a cidade! " + ex.Message);
        }
    }

    public async Task<Response<Cidade?>> ListarCidadePorIdAsync(ListarCidadePorIdRequest request)
    {
        try
        {
            var cidade = await context
                .Cidades
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return cidade is null
                ? new Response<Cidade?>(null, 404, "[CID006] Cidade não encontrada!")
                : new Response<Cidade?>(cidade);
        }
        catch (Exception ex)
        {
            return new Response<Cidade?>(null, 500, "[CID007] Falha ao listar a cidade! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Cidade>?>> ListarCidadesPorNomeAsync(ListarCidadesPorNomeRequest request)
    {
        try
        {
            var consulta = context
            .Cidades
            .AsNoTracking()
            .Where(x => x.Nome.Contains(request.Nome))
            .OrderBy(x => x.UnidadeFederativa.Sigla);

            var cidades = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Cidade>?>(
                    cidades,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Cidade>?>(null, 500, "[CID010] Falha ao listar a(s) cidade(s) por nome! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Cidade>?>> ListarCidadesPorUnidadeFederativaAsync(ListarCidadesPorUnidadeFederativaRequest request)
    {
        try
        {
            var consulta = context
            .Cidades
            .AsNoTracking()
            .Where(x => x.UnidadeFederativaId == request.UnidadeFederativaId)
            .OrderBy(x => x.UnidadeFederativa.Sigla);

            var cidades = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Cidade>?>(
                    cidades,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Cidade>?>(null, 500, "[CID011] Falha ao listar a(s) cidade(s) por unidade federativa! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Cidade>?>> ListarTodasCidadesAsync(ListarTodasCidadesRequest request)
    {
        try
        {
            var consulta = context
            .Cidades
            .AsNoTracking()            
            .OrderBy(x => x.UnidadeFederativa.Sigla);

            var cidades = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Cidade>?>(
                    cidades,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Cidade>?>(null, 500, "[CID012] Falha ao listar a(s) cidade(s)! " + ex.Message);
        }
    }
}
