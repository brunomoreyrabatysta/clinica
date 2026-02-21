using Microsoft.AspNetCore.Components;
using MudBlazor;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.SituacaoCurso;

namespace Clinica.Web.Pages.SituacoesCurso;

public partial class ListarSituacaoCursoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;

    public List<SituacaoCurso> SituacoesCurso { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null;

    [Inject]
    public ISituacaoCursoHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new ListarTodasSituacaoCursoRequest();
            var result = await Handler.ListarTodasAsync(request);
            if (result.Sucesso)
                SituacoesCurso = result.Dados ?? [];
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
    public async void OnDeleteButtonClickedAsync(int id, string titulo)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Deseja realmente excluir a situação de curso '{titulo}'?",
            yesText: "excluir", cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, titulo);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(int id, string titulo)
    {
        IsBusy = true;
        try
        {
            var request = new ExcluirSituacaoCursoRequest { Id = id };
            var response = await Handler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                SituacoesCurso.RemoveAll(tc => tc.Id == id);
                Snackbar.Add(response.Mensagem ?? $"Situação de curso ({titulo}) excluída com sucesso.", Severity.Success);
            }
            else
                Snackbar.Add(response.Mensagem ?? "Falha ao excluir a situação de curso.", Severity.Error);
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
    public Func<SituacaoCurso, bool> Filter => situacaoCurso =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (situacaoCurso.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (situacaoCurso.Titulo.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (situacaoCurso.Descricao is not null && situacaoCurso.Descricao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}
