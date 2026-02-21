using Microsoft.AspNetCore.Components;
using MudBlazor;
using Clinica.Core.Handlers;
using Clinica.Core.Requests.Account;
using Clinica.Web.Security;

namespace Clinica.Web.Pages.Identity;

public partial class LoginPage :ComponentBase
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
    public LoginRequest InputModel { get; set; } = new();
    #endregion

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }
    #endregion

    #region Methods
    public async Task OnValidaSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.LoginAsync(InputModel);

            if (result.Sucesso)
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                NavigationManager.NavigateTo("/");
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
