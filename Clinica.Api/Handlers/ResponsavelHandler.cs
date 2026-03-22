using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Handlers;

public class ResponsavelHandler(AppDbContext context) : IResponsavelHandler
{
    public async Task<Response<Responsavel?>> AlterarAsync(AlterarResponsavelRequest request)
    {
        try
        {
            var responsavel = await context.Responsaveis
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (responsavel is null)
                return new Response<Responsavel?>(null, 404, "[RES002] Responsável não encontrado!");

            responsavel.Nome = request.Nome;
            responsavel.CPF = request.CPF;
            responsavel.RG = request.RG;
            responsavel.DataEmisssaoRG = request.DataEmisssaoRG;
            responsavel.UFEmissaoRG = request.UFEmissaoRG;
            responsavel.Endereco = request.Endereco;
            responsavel.Complemento = request.Complemento;
            responsavel.Numero = request.Numero;
            responsavel.Bairro = request.Bairro;
            responsavel.CidadeId = request.CidadeId;
            responsavel.CEP = request.CEP;
            responsavel.Naturalidade = request.Naturalidade;
            responsavel.Nacionalidade = request.Nacionalidade;
            responsavel.Sexo = request.Sexo;
            responsavel.DataNascimento = request.DataNascimento;
            responsavel.NumeroTelefone = request.NumeroTelefone;
            responsavel.Email = request.Email;


            context.Responsaveis.Update(responsavel);
            await context.SaveChangesAsync();

            return new Response<Responsavel?>(responsavel, mensagem: "Responsável alterado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Responsavel?>(null, 500, "[RES003] Falha ao alterar o responsável! " + ex.Message);
        }
    }

    public async Task<Response<Responsavel?>> CriarAsync(CriarResponsavelRequest request)
    {
        try
        {
            var responsavel = new Responsavel
            {
                Nome = request.Nome,
                CPF = request.CPF,
                RG = request.RG,
                DataEmisssaoRG = request.DataEmisssaoRG,
                UFEmissaoRG = request.UFEmissaoRG,
                Endereco = request.Endereco,
                Complemento = request.Complemento,
                Numero = request.Numero,
                Bairro = request.Bairro,
                CidadeId = request.CidadeId,
                CEP = request.CEP,
                Nacionalidade = request.Nacionalidade,
                Naturalidade = request.Naturalidade,
                Sexo = request.Sexo,
                DataNascimento = request.DataNascimento,
                NumeroTelefone = request.NumeroTelefone,
                Email = request.Email
            };

            await context.Responsaveis.AddAsync(responsavel);
            await context.SaveChangesAsync();

            return new Response<Responsavel?>(responsavel, 201, "Responsável criado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Responsavel?>(null, 500, "[RES001] Falha ao criar o responsável! " + ex.Message);
        }
    }

    public async Task<Response<Responsavel?>> ExcluirAsync(ExcluirResponsavelRequest request)
    {
        try
        {
            var responsavel = await context.Responsaveis
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (responsavel is null)
                return new Response<Responsavel?>(null, 404, "[RES004] Responsável não encontrado!");

            context.Responsaveis.Remove(responsavel);
            await context.SaveChangesAsync();

            return new Response<Responsavel?>(responsavel, mensagem: "Responsável excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Responsavel?>(null, 500, "[RES005] Falha ao excluir o Responsável! " + ex.Message);
        }
    }

    public async Task<Response<Responsavel?>> ListarResponsavelPorIdAsync(ListarResponsavelPorIdRequest request)
    {
        try
        {
            var responsavel = await context
                .Responsaveis
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return responsavel is null
                ? new Response<Responsavel?>(null, 404, "[RES006] Responsável não encontrado!")
                : new Response<Responsavel?>(responsavel);
        }
        catch (Exception ex)
        {
            return new Response<Responsavel?>(null, 500, "[RES007] Falha ao listar o responsável! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Responsavel>?>> ListarResponsaveisPorNomeAsync(ListarResponsaveisPorNomeRequest request)
    {
        try
        {
            var consulta = context
            .Responsaveis
            .AsNoTracking()
            .Where(x => x.Nome.Contains(request.Nome))
            .OrderBy(x => x.Nome);

            var responsaveis = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Responsavel>?>(
                    responsaveis,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Responsavel>?>(null, 500, "[RES009] Falha ao listar o(s) responsável(is) por nome! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Responsavel>?>> ListarTodosResponsaveisAsync(ListarTodosResponsaveisRequest request)
    {
        try
        {
            var consulta = context
                .Responsaveis
                .AsNoTracking()
                .OrderBy(x => x.Nome);

            var responsaveis = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Responsavel>?>(
                responsaveis,
                quantidade,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Responsavel>?>(null, 500, "[RES008] Falha ao listar o(s) responsável(is)! " + ex.Message);
        }
    }
}
