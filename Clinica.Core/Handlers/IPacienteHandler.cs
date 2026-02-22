using Clinica.Core.Models;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface IPacienteHandler
{
    Task<Response<Paciente?>> CriarAsync(CriarPacienteRequest request);
    Task<Response<Paciente?>> AlterarAsync(AlterarPacienteRequest request);
    Task<Response<Paciente?>> ExcluirAsync(ExcluirPacienteRequest request);
    Task<Response<Paciente?>> ListarPacientePorIdAsync(ListarPacientePorIdRequest request);
    Task<PaginacaoResponse<List<Paciente>?>> ListarPacientesPorNomeAsync(ListarPacientesPorNomeRequest request);
    Task<PaginacaoResponse<List<Paciente>?>> ListarTodosPacientesAsync(ListarTodosPacientesRequest request);
}
