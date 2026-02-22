using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Handlers;

public class PacienteHandler(AppDbContext context) : IPacienteHandler
{
    public async Task<Response<Paciente?>> AlterarAsync(AlterarPacienteRequest request)
    {
        try
        {
            var paciente = await context.Pacientes
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (paciente is null)
                return new Response<Paciente?>(null, 404, "[PAC002] Paciente não encontrado!");

            paciente.Nome = request.Nome;
            paciente.CPF = request.CPF;
            paciente.RG = request.RG;
            paciente.DataEmisssaoRG = request.DataEmisssaoRG;
            paciente.UFEmissaoRG = request.UFEmissaoRG;
            paciente.Endereco = request.Endereco;
            paciente.Complemento = request.Complemento;
            paciente.Numero = request.Numero;
            paciente.Bairro = request.Bairro;
            paciente.CidadeId = request.CidadeId;
            paciente.CEP = request.CEP;
            paciente.Naturalidade = request.Naturalidade;
            paciente.Nacionalidade = request.Nacionalidade;
            paciente.Sexo = request.Sexo;
            paciente.DataNascimento = request.DataNascimento;
            paciente.NumeroTelefone = request.NumeroTelefone;
            paciente.Email = request.Email;


            context.Pacientes.Update(paciente);
            await context.SaveChangesAsync();

            return new Response<Paciente?>(paciente, mensagem: "Paciente alterado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Paciente?>(null, 500, "[PAC03] Falha ao alterar o pcaiente! " + ex.Message);
        }
    }

    public async Task<Response<Paciente?>> CriarAsync(CriarPacienteRequest request)
    {
        try
        {
            var paciente = new Paciente
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

            await context.Pacientes.AddAsync(paciente);
            await context.SaveChangesAsync();

            return new Response<Paciente?>(paciente, 201, "Paciente criado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Paciente?>(null, 500, "[PAC001] Falha ao criar o paciente! " + ex.Message);
        }
    }

    public async Task<Response<Paciente?>> ExcluirAsync(ExcluirPacienteRequest request)
    {
        try
        {
            var paciente = await context.Pacientes
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (paciente is null)
                return new Response<Paciente?>(null, 404, "[PAC004] Paciente não encontrado!");

            context.Pacientes.Remove(paciente);
            await context.SaveChangesAsync();

            return new Response<Paciente?>(paciente, mensagem: "Paciente excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Paciente?>(null, 500, "[PAC05] Falha ao excluir o paciente! " + ex.Message);
        }
    }

    public async Task<Response<Paciente?>> ListarPacientePorIdAsync(ListarPacientePorIdRequest request)
    {
        try
        {
            var paciente = await context
                .Pacientes
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return paciente is null
                ? new Response<Paciente?>(null, 404, "[PAC06] Paciente não encontrado!")
                : new Response<Paciente?>(paciente);
        }
        catch (Exception ex)
        {
            return new Response<Paciente?>(null, 500, "[PAC007] Falha ao listar o paciente! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Paciente>?>> ListarPacientesPorNomeAsync(ListarPacientesPorNomeRequest request)
    {
        try
        {
            var consulta = context
            .Pacientes
            .AsNoTracking()
            .Where(x => x.Nome.Contains(request.Nome))
            .OrderBy(x => x.Nome);

            var pacientes = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Paciente>?>(
                    pacientes,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Paciente>?>(null, 500, "[PAC010] Falha ao listar o(s) paciente(s) por nome! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Paciente>?>> ListarTodosPacientesAsync(ListarTodosPacientesRequest request)
    {
        try
        {
            var consulta = context
                .Pacientes
                .AsNoTracking()
                .OrderBy(x => x.Nome);

            var pacientes = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Paciente>?>(
                pacientes,
                quantidade,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Paciente>?>(null, 500, "[UF008] Falha ao listar o(s) paciente(s)! " + ex.Message);
        }
    }
}
