using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Vinculos;

public partial class CriarVinculoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarVinculoRequest InputModel { get; set; } = new();
    
    #endregion

    #region Services
    [Inject]
    public IVinculoHandler Handler { get; set; } = null!;

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
                Snackbar.Add(result.Mensagem ?? "Vínculo criada com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/vinculo");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar o vinculo.", Severity.Error);
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
