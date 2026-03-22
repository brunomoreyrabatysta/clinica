using Clinica.Api.Data;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Clinica.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Clinica.Api.Handlers;

public class VinculoHandler(AppDbContext context) : IVinculoHandler
{
    public async Task<Response<Vinculo?>> AlterarAsync(AlterarVinculoRequest request)
    {
        try
        {
            var vinculo = await context.Vinculos
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (vinculo is null)
                return new Response<Vinculo?>(null, 404, "[VIC002] Vincúlo não encontrado!");

            vinculo.Nome = request.Nome;


            context.Vinculos.Update(vinculo);
            await context.SaveChangesAsync();

            return new Response<Vinculo?>(vinculo, mensagem: "Vínculo alterado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Vinculo?>(null, 500, "[VIC003] Falha ao alterar o vínculo! " + ex.Message);
        }
    }

    public async Task<Response<Vinculo?>> CriarAsync(CriarVinculoRequest request)
    {
        try
        {
            var vinculo = new Vinculo
            {
                Nome = request.Nome
            };

            await context.Vinculos.AddAsync(vinculo);
            await context.SaveChangesAsync();

            return new Response<Vinculo?>(vinculo, 201, "Vínculo criado com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Vinculo?>(null, 500, "[VIC001] Falha ao criar o vínculo! " + ex.Message);
        }
    }

    public async Task<Response<Vinculo?>> ExcluirAsync(ExcluirVinculoRequest request)
    {
        try
        {
            var vinculo = await context.Vinculos
                                .Where(x => x.Id == request.Id)
                                .FirstOrDefaultAsync();

            if (vinculo is null)
                return new Response<Vinculo?>(null, 404, "[VIC004] Vínculo não encontrado!");

            context.Vinculos.Remove(vinculo);
            await context.SaveChangesAsync();

            return new Response<Vinculo?>(vinculo, mensagem: "Vínculo excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return new Response<Vinculo?>(null, 500, "[VIC005] Falha ao excluir o vínculo! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Vinculo>?>> ListarTodosVinculosAsync(ListarTodosVinculosRequest request)
    {
        try
        {
            var consulta = context
                .Vinculos
                .AsNoTracking()
                .OrderBy(x => x.Nome);

            var vinculos = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Vinculo>?>(
                vinculos,
                quantidade,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Vinculo>?>(null, 500, "[VIC008] Falha ao listar o(s) vínculo(s)! " + ex.Message);
        }
    }

    public async Task<Response<Vinculo?>> ListarVinculoPorIdAsync(ListarVinculoPorIdRequest request)
    {
        try
        {
            var vinculo = await context
                .Vinculos
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return vinculo is null
                ? new Response<Vinculo?>(null, 404, "[VIC006] Vínculo não encontrado!")
                : new Response<Vinculo?>(vinculo);
        }
        catch (Exception ex)
        {
            return new Response<Vinculo?>(null, 500, "[VIC007] Falha ao listar o vínculo! " + ex.Message);
        }
    }

    public async Task<PaginacaoResponse<List<Vinculo>?>> ListarVinculosPorNomeAsync(ListarVinculosPorNomeRequest request)
    {
        try
        {
            var consulta = context
            .Vinculos
            .AsNoTracking()
            .Where(x => x.Nome.Contains(request.Nome))
            .OrderBy(x => x.Nome);

            var vinculos = await consulta
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var quantidade = await consulta
                .CountAsync();

            return new PaginacaoResponse<List<Vinculo>?>(
                    vinculos,
                    quantidade,
                    request.NumeroPagina,
                    request.TamanhoPagina);
        }
        catch (Exception ex)
        {
            return new PaginacaoResponse<List<Vinculo>?>(null, 500, "[VIC009] Falha ao listar o(s) vínculo(s) por nome! " + ex.Message);
        }
    }
}
