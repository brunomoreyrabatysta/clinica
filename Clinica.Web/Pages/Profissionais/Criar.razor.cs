using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Requests.UnidadesFederativas;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Profissionais;

public partial class CriarProfissionalPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarProfissionalRequest InputModel { get; set; } = new();

    public List<UnidadeFederativa> UnidadesFederativas { get; set; } = [];
    #endregion

    #region Services
    [Inject]
    public IProfissionalHandler Handler { get; set; } = null!;

    [Inject]
    public IUnidadeFederativaHandler UnidadeFederativaHandler { get; set; } = null!;

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
            await CarregarUnidadesFederativasAsync();
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
            var result = await Handler.CriarAsync(InputModel);
            if (result.Sucesso)
            {
                Snackbar.Add(result.Mensagem ?? "Profissional criado com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/profissional");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar o profissional.", Severity.Error);
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

    #region Private Methods
    private async Task CarregarUnidadesFederativasAsync()
    {
        try
        {
            var request = new ListarTodasUnidadesFederativasRequest();
            var result = await UnidadeFederativaHandler.ListarTodasUnidadesFederativasAsync(request);
            if (result.Sucesso)
            {
                UnidadesFederativas = result.Dados ?? [];
                InputModel.UnidadeFederativaId = UnidadesFederativas.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar as unidades federativas.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}