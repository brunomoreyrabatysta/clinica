using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Requests.UnidadesFederativas;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Profissionais;

public partial class EditarProfissionalPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public AlterarProfissionalRequest InputModel { get; set; } = new();
    public List<UnidadeFederativa> UnidadesFederativas { get; set; } = [];
    #endregion

    #region Parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;
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

    #region Overrides
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            await CarregarUnidadeFederativaAsync();
            await CarregarProfissionalPorIdAsync();
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
                Snackbar.Add("Profissional alterado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/profissional");
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Não foi possível alterar o profissional.", Severity.Error);
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

    private async Task CarregarProfissionalPorIdAsync()
    {
        try
        {
            var id = int.Parse(Id);
            var request = new ListarProfissionalPorIdRequest
            {
                Id = id
            };

            var result = await Handler.ListarProfissionalPorIdAsync(request);
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarProfissionalRequest
                {
                    Id = result.Dados.Id,
                    Nome = result.Dados.Nome,
                    Tipo = result.Dados.Tipo,
                    NumeroTelefone = result.Dados.NumeroTelefone,
                    Email = result.Dados.Email,
                    CPF = result.Dados.CPF,
                    NumeroRegistro = result.Dados.NumeroRegistro,
                    UnidadeFederativaId = result.Dados.UnidadeFederativaId,
                    Conselho = result.Dados.Conselho,
                    Situacao = result.Dados.Situacao
                };
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Profissional não encontrado.", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}