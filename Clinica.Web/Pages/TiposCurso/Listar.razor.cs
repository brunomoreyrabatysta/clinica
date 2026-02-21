using Microsoft.AspNetCore.Components;
using MudBlazor;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.TiposCurso;

namespace Clinica.Web.Pages.TiposCurso;

public partial class ListarTipoCursoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    
    public List<TipoCurso> TiposCurso { get; set; } = [];

    public string SearchTerm { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null;

    [Inject]
    public ITipoCursoHandler Handler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new ListarTodosTipoCursoRequest();
            var result = await Handler.ListarTodosAsync(request);
            if (result.Sucesso)
                TiposCurso = result.Dados ?? [];
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
            $"Deseja realmente excluir o tipo de curso '{titulo}'?",
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
            var request = new ExcluirTipoCursoRequest { Id = id };
            var response = await Handler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                TiposCurso.RemoveAll(tc => tc.Id == id);
                Snackbar.Add(response.Mensagem ?? $"Tipo de curso ({titulo}) excluído com sucesso.", Severity.Success);
            }
            else
                Snackbar.Add(response.Mensagem ?? "Falha ao excluir o tipo de curso.", Severity.Error);
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
    public Func<TipoCurso, bool> Filter => tipoCurso =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;

        if (tipoCurso.Id.ToString().Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (tipoCurso.Titulo.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        if (tipoCurso.Descricao is not null && tipoCurso.Descricao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    #endregion
}
