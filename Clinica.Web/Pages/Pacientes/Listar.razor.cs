using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Pacientes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Pacientes;

public partial class ListarPacientePage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;

    public List<Paciente> pacientes { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null;

    [Inject]
    public IPacienteHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new ListarTodosPacientesRequest();
            var result = await Handler.ListarTodosPacientesAsync(request);
            if (result.Sucesso)
                pacientes = result.Dados ?? [];
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
    public async void OnDeleteButtonClickedAsync(long id, string nome)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Deseja realmente excluir o paciente '{nome}'?",
            yesText: "excluir", cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, nome);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string nome)
    {
        IsBusy = true;
        try
        {
            var request = new ExcluirPacienteRequest { Id = id };
            var response = await Handler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                pacientes.RemoveAll(tc => tc.Id == id);
                Snackbar.Add(response.Mensagem ?? $"Paciente ({nome}) excluído com sucesso.", Severity.Success);
            }
            else
                Snackbar.Add(response.Mensagem ?? "Falha ao excluir o paciente.", Severity.Error);
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
    public Func<Paciente, bool> Filter => apci =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (apci.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (apci.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}
