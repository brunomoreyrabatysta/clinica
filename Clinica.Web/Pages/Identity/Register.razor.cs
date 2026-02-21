using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Clinica.Core.Handlers;
using Clinica.Core.Requests.Account;
using Clinica.Web.Security;

namespace Clinica.Web.Pages.Identity;

public partial class RegisterPage: ComponentBase
{
    #region Dependências
    [Inject]
    public ISnackbar Snackbar { get; set; } = null;

    [Inject]
    public IAccountHandler Handler { get; set; } = null;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null;

    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null;
    #endregion

    #region Proprties
    public bool IsBusy { get; set; } = false;
    public RegisterRequest InputModel { get; set; } = new();
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is {IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }
    #endregion

    #region Methods
    public async Task OnValidaSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.RegisterAsync(InputModel);

            if (result.Sucesso)
            {
                Snackbar.Add(result.Mensagem, Severity.Success);
                NavigationManager.NavigateTo("/login");
            }                
            else
                Snackbar.Add(result.Mensagem, Severity.Error);
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
