using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Vinculos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Vinculos;

public partial class EditarVinculoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public AlterarVinculoRequest InputModel { get; set; } = new();    
    #endregion

    #region Parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public IVinculoHandler Handler { get; set; } = null!;

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
            await CarregarVinculoPorIdAsync();
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
                Snackbar.Add("Vínculo alterado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/vinculo");
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Não foi possível alterar o vínculo.", Severity.Error);
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

    #region Private Methods
    private async Task CarregarVinculoPorIdAsync()
    {
        try
        {
            var id = int.Parse(Id);
            var request = new ListarVinculoPorIdRequest
            {
                Id = id
            };

            var result = await Handler.ListarVinculoPorIdAsync(request);
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarVinculoRequest
                {
                    Id = result.Dados.Id,
                    Nome = result.Dados.Nome
                };
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Vínculo não encontrado.", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}