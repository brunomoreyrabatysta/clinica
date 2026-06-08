using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Requests.Prontuarios;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Prontuarios;

public partial class CriarProntuarioPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarProntuarioRequest InputModel { get; set; } = new();

    public List<Contrato> Contratos { get; set; } = [];
    public List<Profissional> Profissionais { get; set; } = [];
    #endregion

    #region Services
    [Inject]
    public IProntuarioHandler Handler { get; set; } = null!;

    [Inject]
    public IContratoHandler ContratoHandler { get; set; } = null!;

    [Inject]
    public IProfissionalHandler ProfissionalHandler { get; set; } = null!;

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
            await CarregarContratosAsync();
            await CarregarProfissionaisAsync();
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
                Snackbar.Add(result.Mensagem ?? "Prontuário criado com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/prontuario");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar o prontuário.", Severity.Error);
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
    private async Task CarregarContratosAsync()
    {
        try
        {
            var request = new ListarTodosContratosRequest();
            var result = await ContratoHandler.ListarTodosContratosAsync(request);
            if (result.Sucesso)
            {
                Contratos = result.Dados ?? [];
                InputModel.ContratoId = Contratos.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os contratos.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    private async Task CarregarProfissionaisAsync()
    {
        try
        {
            var request = new ListarTodosProfissionaisRequest();
            var result = await ProfissionalHandler.ListarTodosProfissionaisAsync(request);
            if (result.Sucesso)
            {
                Profissionais = result.Dados ?? [];
                InputModel.ProfissionalId = Profissionais.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os profissionais.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}
