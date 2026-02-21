using Microsoft.AspNetCore.Components;
using MudBlazor;
using Clinica.Core.Handlers;
using Clinica.Core.Requests.TiposCurso;

namespace Clinica.Web.Pages.TiposCurso;

public partial class CriarTipoCursoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarTipoCursoRequest InputModel { get; set; } = new ();
    #endregion

    #region Services
    [Inject]
    public ITipoCursoHandler Handler { get; set; } = null!;

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
                Snackbar.Add(result.Mensagem ?? "Tipo de curso criado com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/tipocurso");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar o tipo de curso.", Severity.Error);
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
