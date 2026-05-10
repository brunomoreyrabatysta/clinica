using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Profissionais;

public partial class ListarProfissionalPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;

    public List<Profissional> Profissionais { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null;

    [Inject]
    public IProfissionalHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new ListarTodosProfissionaisRequest();
            var result = await Handler.ListarTodosProfissionaisAsync(request);
            if (result.Sucesso)
                Profissionais = result.Dados ?? [];
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
            $"Deseja realmente excluir o profissional '{nome}'?",
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
            var request = new ExcluirProfissionalRequest { Id = id };
            var response = await Handler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                Profissionais.RemoveAll(tc => tc.Id == id);
                Snackbar.Add(response.Mensagem ?? $"Profissional ({nome}) excluído com sucesso.", Severity.Success);
            }
            else
                Snackbar.Add(response.Mensagem ?? "Falha ao excluir o profissional.", Severity.Error);
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
    public Func<Profissional, bool> Filter => profissionais =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (profissionais.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (profissionais.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}
