using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Cidades;

public partial class ListarCidadePage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;

    public List<Cidade> Cidades { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null;

    [Inject]
    public ICidadeHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new ListarTodasCidadesRequest();
            var result = await Handler.ListarTodasCidadesAsync(request);
            if (result.Sucesso)
                Cidades = result.Dados ?? [];
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
            $"Deseja realmente excluir a cidade '{nome}'?",
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
            var request = new ExcluirCidadeRequest { Id = id };
            var response = await Handler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                Cidades.RemoveAll(tc => tc.Id == id);
                Snackbar.Add(response.Mensagem ?? $"Cidade ({nome}) excluída com sucesso.", Severity.Success);
            }
            else
                Snackbar.Add(response.Mensagem ?? "Falha ao excluir a cidade.", Severity.Error);
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
    public Func<Cidade, bool> Filter => cidades =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (cidades.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (cidades.Nome.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}
