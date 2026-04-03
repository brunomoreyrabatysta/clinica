using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Requests.Vinculos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Contratos;

public partial class CriarContratoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarContratoRequest InputModel { get; set; } = new();

    public List<Paciente> Pacientes { get; set; } = [];
    public List<Responsavel> Responsaveis { get; set; } = [];
    public List<Vinculo> Vinculos { get; set; } = [];
    #endregion

    #region Services
    [Inject]
    public IContratoHandler Handler { get; set; } = null!;

    [Inject]
    public IPacienteHandler PacienteHandler { get; set; } = null!;

    [Inject]
    public IResponsavelHandler ResponsavelHandler { get; set; } = null!;
    
    [Inject]
    public IVinculoHandler VinculoHandler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region Override
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            await CarregarPacientesAsync();
            await CarregarResponsaveisAsync();
            await CarregarVinculosAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.CriarAsync(InputModel);
            if (result.Sucesso)
            {
                Snackbar.Add(result.Mensagem ?? "Contrato criado com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/contrato");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar o contrato.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

    #region Private Methods
    private async Task CarregarPacientesAsync()
    {
        try
        {
            var request = new ListarTodosPacientesRequest();
            var result = await PacienteHandler.ListarTodosPacientesAsync(request);
            if (result.Sucesso)
            {
                Pacientes = result.Dados ?? [];
                InputModel.PacienteId = Pacientes.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os pacientes.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task CarregarResponsaveisAsync()
    {
        try
        {
            var request = new ListarTodosResponsaveisRequest();
            var result = await ResponsavelHandler.ListarTodosResponsaveisAsync(request);
            if (result.Sucesso)
            {
                Responsaveis = result.Dados ?? [];
                InputModel.ResponsavelId = Responsaveis.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os responsáveis.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task CarregarVinculosAsync()
    {
        try
        {
            var request = new ListarTodosVinculosRequest();
            var result = await VinculoHandler.ListarTodosVinculosAsync(request);
            if (result.Sucesso)
            {
                Vinculos = result.Dados ?? [];
                InputModel.VinculoId = Vinculos.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os vínculos.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}
