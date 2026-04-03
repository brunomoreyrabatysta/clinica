using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Contratos;

public partial class ListarContratoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;

    public List<Contrato> Contratos { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null;

    [Inject]
    public IContratoHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new ListarTodosContratosRequest();
            var result = await Handler.ListarTodosContratosAsync(request);
            if (result.Sucesso)
                Contratos = result.Dados ?? [];
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
            $"Deseja realmente excluir o contrato '{nomePaciente} - {nomeResponsavel}'?",
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
            var request = new ExcluirContratoRequest { Id = id };
            var response = await Handler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                Contratos.RemoveAll(tc => tc.Id == id);
                Snackbar.Add(response.Mensagem ?? $"Contrato ({nomePaciente} - {nomeResponsavel}) excluído com sucesso.", Severity.Success);
            }
            else
                Snackbar.Add(response.Mensagem ?? "Falha ao excluir o contrato.", Severity.Error);
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
    public Func<Contrato, bool> Filter => contratos =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (contratos.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (contratos.Paciente.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (contratos.Responsavel.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}
