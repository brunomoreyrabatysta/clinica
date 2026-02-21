using Microsoft.AspNetCore.Components;
using MudBlazor;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Curso;
using Clinica.Core.Requests.SituacaoCurso;
using Clinica.Core.Requests.TiposCurso;

namespace Clinica.Web.Pages.Cursos;

public partial class EditarCursoPage : ComponentBase
{
    #region Properties
    [Parameter]
    public string Id { get; set; } = string.Empty;
    public bool IsBusy { get; set; } = false;
    public AlterarCursoRequest InputModel { get; set; } = new AlterarCursoRequest();    
    public List<TipoCurso> TiposCurso { get; set; } = [];
    public List<SituacaoCurso> SituacoesCurso { get; set; } = [];
    #endregion

    #region Services
    [Inject]
    public ICursoHandler CursoHandler { get; set; } = null!;

    [Inject]
    public ITipoCursoHandler TipoCursoHandler { get; set; } = null!;

    [Inject]
    public ISituacaoCursoHandler SituacaoCursoHandler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region Override
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            await CarregarCursoPorIdAsync();
            await CarregarTiposCursoAsync();
            await CarregarSituacoesCursoAsync();
        }
        finally
        {
            IsBusy = false;
        }        
    }
    #endregion

    #region Private Methods
    private async Task CarregarTiposCursoAsync()
    {        
        try
        {
            var request = new ListarTodosTipoCursoRequest();
            var result = await TipoCursoHandler.ListarTodosAsync(request);
            if (result.Sucesso)
            {
                TiposCurso = result.Dados ?? [];
                InputModel.TipoCursoId = TiposCurso.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os tipos de curso.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task CarregarSituacoesCursoAsync()
    {
        try
        {
            var request = new ListarTodasSituacaoCursoRequest();
            var result = await SituacaoCursoHandler.ListarTodasAsync(request);
            if (result.Sucesso)
            {
                SituacoesCurso = result.Dados ?? [];
                InputModel.SituacaoCursoId = SituacoesCurso.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar as situações de curso.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task CarregarCursoPorIdAsync()
    {        
        try
        {
            var request = new ListarCursoPorIdRequest
            {
                Id = long.Parse(Id)
            };

            var result = await CursoHandler.ListarCursoPorIdAsync(request);            
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarCursoRequest
                {
                    Id = result.Dados.Id,
                    Titulo = result.Dados.Titulo,
                    Descricao = result.Dados.Descricao,
                    TipoCursoId = result.Dados.TipoCursoId,
                    UrlCredencial = result.Dados.UrlCredencial,
                    DataInicio = result.Dados.DataInicio,
                    DataTermino = result.Dados.DataTermino,
                    DataExpiracao = result.Dados.DataExpiracao,
                    DataEmissao = result.Dados.DataEmissao,
                    CargaHoraria = result.Dados.CargaHoraria,
                    CodigoCredencial = result.Dados.CodigoCredencial,
                    Competencia = result.Dados.Competencia,
                    OrganizacaoEmissora = result.Dados.OrganizacaoEmissora,
                    SituacaoCursoId = result.Dados.SituacaoCursoId
                };
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar o curso.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await CursoHandler.AlterarAsync(InputModel);
            if (result.Sucesso)
            {
                Snackbar.Add(result.Mensagem ?? "Curso alterado com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/curso");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao alterar o curso.", Severity.Error);
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
}
