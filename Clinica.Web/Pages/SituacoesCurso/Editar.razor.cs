using Microsoft.AspNetCore.Components;
using MudBlazor;
using Clinica.Core.Handlers;
using Clinica.Core.Requests.SituacaoCurso;

namespace Clinica.Web.Pages.SituacoesCurso;

public partial class EditarSituacaoCursoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public AlterarSituacaoCursoRequest InputModel { get; set; } = new();
    #endregion

    #region Parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ISituacaoCursoHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var id = int.Parse(Id);
            var request = new ListarSituacaoCursoPorIdRequest
            {
                Id = id
            };

            var result = await Handler.ListarPorIdAsync(request);
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarSituacaoCursoRequest
                {
                    Id = result.Dados.Id,
                    Titulo = result.Dados.Titulo,
                    Descricao = result.Dados.Descricao
                };
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Situação de curso não encontrada.", Severity.Error);
            }
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
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.AlterarAsync(InputModel);
            if (result.Sucesso)
            {
                Snackbar.Add("Situação de curso alterada com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/situacaocurso");
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Não foi possível alterar a situação de curso.", Severity.Error);
            }
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
