using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Clinica.Core.Requests.Responsaveis;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Responsaveis;

public partial class CriarResponsavelPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarResponsavelRequest InputModel { get; set; } = new();

    public List<Cidade> Cidades { get; set; } = [];
    #endregion

    #region Services
    [Inject]
    public IResponsavelHandler Handler { get; set; } = null!;

    [Inject]
    public ICidadeHandler CidadeHandler { get; set; } = null!;

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
            await CarregarCidadesAsync();
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
                Snackbar.Add(result.Mensagem ?? "Responsável criado com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/responsavel");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar o responsável.", Severity.Error);
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
    private async Task CarregarCidadesAsync()
    {
        try
        {
            var request = new ListarTodasCidadesRequest();
            var result = await CidadeHandler.ListarTodasCidadesAsync(request);
            if (result.Sucesso)
            {
                Cidades = result.Dados ?? [];
                InputModel.CidadeId = Cidades.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar as cidades.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}