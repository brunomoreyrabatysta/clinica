using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Clinica.Core.Requests.UnidadesFederativas;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Cidades;

public partial class EditarCidadePage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public AlterarCidadeRequest InputModel { get; set; } = new();
    public List<UnidadeFederativa> UnidadesFederativas { get; set; } = [];
    #endregion

    #region Parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public ICidadeHandler Handler { get; set; } = null!;

    [Inject]
    public IUnidadeFederativaHandler UnidadeFederativaHandler { get; set; } = null!;

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
            await CarregarCidadePorIdAsync();
            await CarregarUnidadeFederativaAsync();
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
                Snackbar.Add("Cidade alterada com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/cidade");
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Não foi possível alterar a cidade.", Severity.Error);
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
    private async Task CarregarUnidadeFederativaAsync()
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

    private async Task CarregarCidadePorIdAsync()
    {
        try
        {
            var id = int.Parse(Id);
            var request = new ListarCidadePorIdRequest
            {
                Id = id
            };

            var result = await Handler.ListarCidadePorIdAsync(request);
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarCidadeRequest
                {
                    Id = result.Dados.Id,
                    Nome = result.Dados.Nome,
                    UnidadeFederativaId = result.Dados.UnidadeFederativaId
                };
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Cidade não encontrada.", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}
