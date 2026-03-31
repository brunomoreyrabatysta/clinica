using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Vinculos;

public partial class ListarVinculoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;

    public List<Vinculo> Vinculos { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null;

    [Inject]
    public IVinculoHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new ListarTodosVinculosRequest();
            var result = await Handler.ListarTodosVinculosAsync(request);
            if (result.Sucesso)
                Vinculos = result.Dados ?? [];
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
    public async void OnDeleteButtonClickedAsync(int id, string nome)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Deseja realmente excluir o vínculo '{nome}'?",
            yesText: "excluir", cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, nome);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(int id, string nome)
    {
        IsBusy = true;
        try
        {
            var request = new ExcluirVinculoRequest { Id = id };
            var response = await Handler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                Vinculos.RemoveAll(tc => tc.Id == id);
                Snackbar.Add(response.Mensagem ?? $"Vínculo ({nome}) excluído com sucesso.", Severity.Success);
            }
            else
                Snackbar.Add(response.Mensagem ?? "Falha ao excluir o vínculo.", Severity.Error);
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
    public Func<Vinculo, bool> Filter => vinculos =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (vinculos.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (vinculos.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}
