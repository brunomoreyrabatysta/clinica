using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.UnidadesFederativas;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.UnidadesFederativas;

public partial class ListarUnidadeFederativaPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;

    public List<UnidadeFederativa> UnidadesFederativas { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null;

    [Inject]
    public IUnidadeFederativaHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new ListarTodasUnidadesFederativasRequest();
            var result = await Handler.ListarTodasUnidadesFederativasAsync(request);
            if (result.Sucesso)
                UnidadesFederativas = result.Dados ?? [];
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
            $"Deseja realmente excluir a unidade federativa '{nome}'?",
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
            var request = new ExcluirUnidadeFederativaRequest { Id = id };
            var response = await Handler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                UnidadesFederativas.RemoveAll(tc => tc.Id == id);
                Snackbar.Add(response.Mensagem ?? $"Unidade federativa ({nome}) excluída com sucesso.", Severity.Success);
            }
            else
                Snackbar.Add(response.Mensagem ?? "Falha ao excluir a unidade federativa.", Severity.Error);
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
    public Func<UnidadeFederativa, bool> Filter => unidadeFederativa =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (unidadeFederativa.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (unidadeFederativa.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (unidadeFederativa.Sigla is not null && unidadeFederativa.Sigla.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}
