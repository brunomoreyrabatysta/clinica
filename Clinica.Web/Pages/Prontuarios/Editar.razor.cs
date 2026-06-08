using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Requests.Profissionais;
using Clinica.Core.Requests.Prontuarios;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Prontuarios;

public partial class EditarProntuarioPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public AlterarProntuarioRequest InputModel { get; set; } = new();
    public List<Contrato> Contratos { get; set; } = [];
    public List<Profissional> Profissionais { get; set; } = [];
    #endregion

    #region Parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public IProntuarioHandler Handler { get; set; } = null!;

    [Inject]
    public IContratoHandler ContratoHandler { get; set; } = null!;

    [Inject]
    public IProfissionalHandler ProfissionalHandler { get; set; } = null!;

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
            await CarregarProntuarioPorIdAsync();
            await CarregarContratosAsync();
            await CarregarProfissionaisAsync();
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
                Snackbar.Add("Prontuário alterado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/prontuario");
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Não foi possível alterar o prontuário.", Severity.Error);
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
    private async Task CarregarContratosAsync()
    {
        try
        {
            var request = new ListarTodosContratosRequest();
            var result = await ContratoHandler.ListarTodosContratosAsync(request);
            if (result.Sucesso)
            {
                Contratos = result.Dados ?? [];
                InputModel.ContratoId = Contratos.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os contratos.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    private async Task CarregarProfissionaisAsync()
    {
        try
        {
            var request = new ListarTodosProfissionaisRequest();
            var result = await ProfissionalHandler.ListarTodosProfissionaisAsync(request);
            if (result.Sucesso)
            {
                Profissionais = result.Dados ?? [];
                InputModel.ProfissionalId = Profissionais.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os profissionais.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task CarregarProntuarioPorIdAsync()
    {
        try
        {
            var id = int.Parse(Id);
            var request = new ListarProntuarioPorIdRequest
            {
                Id = id
            };

            var result = await Handler.ListarProntuarioPorIdAsync(request);
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarProntuarioRequest
                {
                    Id = result.Dados.Id,
                    DataProntuario = result.Dados.DataProntuario,
                    Cobranca = result.Dados.Cobranca,
                    Descricao = result.Dados.Descricao,
                    ContratoId = result.Dados.ContratoId,
                    ProfissionalId = result.Dados.ProfissionalId,
                    TempoAtendimento = result.Dados.TempoAtendimento
                };
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Prontuário não encontrado.", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}
