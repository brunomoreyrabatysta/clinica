using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Financeiros;

public partial class ListarFinanceiroPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;

    public List<Financeiro> Financeiros { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null;

    [Inject]
    public IFinanceiroHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new ListarTodosFinanceirosRequest();
            var result = await Handler.ListarTodosFinanceirosAsync(request);
            if (result.Sucesso)
                Financeiros = result.Dados ?? [];
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

    #region Methods
    public async void OnDeleteButtonClickedAsync(long id, string nomePaciente, string nomeResponsavel)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Deseja realmente excluir o financeiro '{nomePaciente} - {nomeResponsavel}'?",
            yesText: "excluir", cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, nomePaciente, nomeResponsavel);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string nomePaciente, string nomeResponsavel)
    {
        IsBusy = true;
        try
        {
            var request = new ExcluirFinanceiroRequest { Id = id };
            var response = await Handler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                Financeiros.RemoveAll(tc => tc.Id == id);
                Snackbar.Add(response.Mensagem ?? $"Financeiro ({nomePaciente} - {nomeResponsavel}) excluído com sucesso.", Severity.Success);
            }
            else
                Snackbar.Add(response.Mensagem ?? "Falha ao excluir o financeiro.", Severity.Error);
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
    public Func<Financeiro, bool> Filter => financeiros =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (financeiros.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (financeiros.Contrato.Paciente.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (financeiros.Contrato.Responsavel.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}
