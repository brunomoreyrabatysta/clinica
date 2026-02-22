using Clinica.Core.Handlers;
using Clinica.Core.Requests.UnidadesFederativas;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.UnidadesFederativas;

public partial class EditarUnidadeFederativaPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public AlterarUnidadeFederativaRequest InputModel { get; set; } = new();
    #endregion

    #region Parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public IUnidadeFederativaHandler Handler { get; set; } = null!;

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
            var id = int.Parse(Id);
            var request = new ListarUnidadeFederativaPorIdRequest
            {
                Id = id
            };

            var result = await Handler.ListarUnidadeFederativaPorIdAsync(request);
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarUnidadeFederativaRequest
                {
                    Id = result.Dados.Id,
                    Nome = result.Dados.Nome,
                    Sigla = result.Dados.Sigla
                };
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Unidade federativa não encontrada.", Severity.Error);
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

    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.AlterarAsync(InputModel);
            if (result.Sucesso)
            {
                Snackbar.Add("Unidade federativa alterada com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/unidadefederativa");
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Não foi possível alterar a unidade federativa.", Severity.Error);
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
}
