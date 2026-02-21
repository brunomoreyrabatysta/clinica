using Microsoft.AspNetCore.Components;
using MudBlazor;
using Clinica.Core.Handlers;
using Clinica.Core.Requests.SituacaoCurso;

namespace Clinica.Web.Pages.SituacoesCurso;

public partial class CriarSituacaoCursoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarSituacaoCursoRequest InputModel { get; set; } = new ();
    #endregion

    #region Services
    [Inject]
    public ISituacaoCursoHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.CriarAsync(InputModel);
            if (result.Sucesso)
            {
                Snackbar.Add(result.Mensagem ?? "Situação de curso criada com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/situacaocurso");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar a situação do curso.", Severity.Error);
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
