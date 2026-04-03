using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Requests.Contratos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Financeiros;

public partial class CriarFinanceiroPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarFinanceiroRequest InputModel { get; set; } = new();

    public List<Contrato> Contratos { get; set; } = [];
    #endregion

    #region Services
    [Inject]
    public IFinanceiroHandler Handler { get; set; } = null!;

    [Inject]
    public IContratoHandler ContratoHandler { get; set; } = null!;

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
                Snackbar.Add(result.Mensagem ?? "Financeiro criado com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/financeiro");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar o financeiro.", Severity.Error);
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
    #endregion
}
