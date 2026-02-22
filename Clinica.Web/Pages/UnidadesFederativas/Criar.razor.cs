using Clinica.Core.Handlers;
using Clinica.Core.Requests.UnidadesFederativas;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.UnidadesFederativas;

public partial class CriarUnidadeFederativaPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarUnidadeFederativaRequest InputModel { get; set; } = new();
    #endregion

    #region Services
    [Inject]
    public IUnidadeFederativaHandler Handler { get; set; } = null!;

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
                Snackbar.Add(result.Mensagem ?? "Unidade federativa criada com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/unidadefederativa");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar a unidade federativa.", Severity.Error);
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
