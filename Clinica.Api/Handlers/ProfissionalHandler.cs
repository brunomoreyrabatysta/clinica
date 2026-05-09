using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Handlers;

public class ProfissionalHandler(AppDbContext context) : IProfissionalHandler
{
    public async Task<Response<Profissional?>> AlterarAsync(AlterarProfissionalRequest request)
    {
        try
        {
            var profissional = await context.Profissionais
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (profissional is null)
                return new Response<Profissional?>(null, 404, "[PROF002] Profissional não encontrado!");

            profissional.Nome = request.Nome;
            profissional.CPF = request.CPF;
            profissional.Tipo = request.Tipo;
            profissional.NumeroTelefone = request.NumeroTelefone;
            profissional.Email = request.Email;
            profissional.NumeroRegistro = request.NumeroRegistro;
            profissional.UnidadeFederativaId = request.UnidadeFederativaId;
            profissional.Conselho = request.Conselho;
            profissional.Situacao = request.Situacao;

            context.Profissionais.Update(profissional);
            await context.SaveChangesAsync();

            return new Response<Profissional?>(profissional, mensagem: "Profissional alterado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Profissional?>(null, 500, "[PROF003] Falha ao alterar o profissional! " + ex.Message);
        }
    }

    public async Task<Response<Profissional?>> CriarAsync(CriarProfissionalRequest request)
    {
        try
        {
            var profissional = new Profissional
            {
                Nome = request.Nome,
                CPF = request.CPF,
                Tipo = request.Tipo,
                NumeroTelefone = request.NumeroTelefone,
                Email = request.Email,
                NumeroRegistro = request.NumeroRegistro,
                UnidadeFederativaId = request.UnidadeFederativaId,
                Conselho = request.Conselho,
                Situacao = request.Situacao
            };

            await context.Profissionais.AddAsync(profissional);
            await context.SaveChangesAsync();

            return new Response<Profissional?>(profissional, 201, "Profissional criado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Profissional?>(null, 500, "[PROF001] Falha ao criar o profissional! " + ex.Message);
        }
    }

    public async Task<Response<Profissional?>> ExcluirAsync(ExcluirProfissionalRequest request)
    {
        try
        {
            var profissional = await context.Profissionais
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (profissional is null)
                return new Response<Profissional?>(null, 404, "[PROF004] Profissional não encontrado!");

            context.Profissionais.Remove(profissional);
            await context.SaveChangesAsync();

            return new Response<Profissional?>(profissional, mensagem: "Profissional excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Profissional?>(null, 500, "[PROF005] Falha ao excluir o profissional! " + ex.Message);
        }
    }

    public async Task<Response<Profissional?>> ListarProfissionalPorIdAsync(ListarProfissionalPorIdRequest request)
    {
        try
        {
            var profissional = await context
                .Profissionais
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return profissional is null
                ? new Response<Profissional?>(null, 404, "[PROF006] Profissional não encontrado!")
                : new Response<Profissional?>(profissional);
        }
        catch (Exception ex)
        {
            return new Response<Profissional?>(null, 500, "[PROF007] Falha ao listar o profissional! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Profissional>?>> ListarProfissionaisPorNomeAsync(ListarProfissionaisPorNomeRequest request)
    {
        try
        {
            var consulta = context
            .Profissionais
            .AsNoTracking()
            .Where(x => x.Nome.Contains(request.Nome))
            .OrderBy(x => x.Nome);

            var profissionais = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Profissional>?>(
                    profissionais,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Profissional>?>(null, 500, "[PROF009] Falha ao listar o(s) profissional(is) por nome! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Profissional>?>> ListarTodosProfissionaisAsync(ListarTodosProfissionaisRequest request)
    {
        try
        {
            var consulta = context
                .Profissionais
                .AsNoTracking()
                .OrderBy(x => x.Nome);

            var profissionais = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Profissional>?>(
                profissionais,
                quantidade,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Profissional>?>(null, 500, "[PROF008] Falha ao listar o(s) profissional(is)! " + ex.Message);
        }
    }
}
