using Microsoft.AspNetCore.Components;
using MudBlazor;
using Clinica.Core.Common.Extensions;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Curso;
using Clinica.Core.Requests.TiposCurso;

namespace Clinica.Web.Pages.Cursos;

public partial class ListarCursoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public List<Curso> Cursos { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;
    public int AnoAtual { get; set; } = DateTime.Now.Year;
    public int MesAtual { get; set; } = DateTime.Now.Month;

    public int[] Anos { get; set; } =
    {
        DateTime.Now.Year,
        DateTime.Now.AddYears(-1).Year,
        DateTime.Now.AddYears(-2).Year,
        DateTime.Now.AddYears(-3).Year,
        DateTime.Now.AddYears(-4).Year
    };

    #endregion

    #region Services
    [Inject]
    public ISnackbar SnackBar { get; set; } = null!;

    [Inject]
    public IDialogService DialogService { get; set; } = null!;

    [Inject]
    public ICursoHandler CursoHandler { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync() => await CarregarCursos();
    #endregion

    #region Methods
    public async void OnDeleteButtonClickedAsync(long id, string titulo)
    {
        var result = await DialogService.ShowMessageBox(
            "ATENÇÃO",
            $"Deseja realmente excluir o curso '{titulo}'?",
            yesText: "excluir", cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, titulo);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string titulo)
    {
        IsBusy = true;
        try
        {
            var request = new ExcluirCursoRequest { Id = id };
            var response = await CursoHandler.ExcluirAsync(request);
            if (response.Sucesso)
            {
                Cursos.RemoveAll(tc => tc.Id == id);
                SnackBar.Add(response.Mensagem ?? $"O curso ({titulo}) excluído com sucesso.", Severity.Success);
            }
            else
                SnackBar.Add(response.Mensagem ?? "Falha ao excluir o curso.", Severity.Error);
        }
        catch (Exception ex)
        {
            SnackBar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    public Func<Curso, bool> Filter => curso =>
    {
        if (string.IsNullOrWhiteSpace(SearchTerm))
            return true;
        if (curso.Titulo.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (curso.OrganizacaoEmissora.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (!string.IsNullOrWhiteSpace(curso.Descricao) && curso.Descricao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    };

    public async Task OnSearchAsync()
    {
        await CarregarCursos();
        StateHasChanged();
    }
    #endregion

    #region Private Method

    private async Task CarregarCursos()
    {
        try
        {
            IsBusy = true;
            var request = new ListarCursoPorPeriodoRequest
            {
                DataInicio = DateTime.Now.ObterPrimeiroDia(AnoAtual, MesAtual),
                DataTermino = DateTime.Now.ObterUltimoDia(AnoAtual, MesAtual),
                NumeroPagina = 1,
                TamanhoPagina = 25
            };
            var response = await CursoHandler.ListarCursoPorPeriodoAsync(request);
            if (response.Sucesso && response.Dados is not null)
            {
                Cursos = response.Dados;
            }
            else
            {
                SnackBar.Add("Não foi possível carregar os cursos.", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            SnackBar.Add($"Ocorreu um erro ao carregar os cursos: {ex.Message}", Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}
