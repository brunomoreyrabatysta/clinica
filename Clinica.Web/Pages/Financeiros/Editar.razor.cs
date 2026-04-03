using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Financeiros;
using Clinica.Core.Requests.Contratos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Financeiros;

public partial class EditarFinanceiroPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public AlterarFinanceiroRequest InputModel { get; set; } = new();
    public List<Contrato> Contratos { get; set; } = [];
    #endregion

    #region Parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public IFinanceiroHandler Handler { get; set; } = null!;

    [Inject]
    public IContratoHandler ContratoHandler { get; set; } = null!;

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
            await CarregarContratosAsync();
            await CarregarFinanceiroPorIdAsync();
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
                Snackbar.Add("Financeiro alterado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/financeiro");
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Não foi possível alterar o financeiro.", Severity.Error);
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

    private async Task CarregarFinanceiroPorIdAsync()
    {
        try
        {
            var id = int.Parse(Id);
            var request = new ListarFinanceiroPorIdRequest
            {
                Id = id
            };

            var result = await Handler.ListarFinanceiroPorIdAsync(request);
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarFinanceiroRequest
                {
                    Id = result.Dados.Id,
                    ContratoId = result.Dados.ContratoId,
                    DataEmissao = result.Dados.DataEmissao,
                    DataVencimento = result.Dados.DataVencimento,
                    DataPagamento = result.Dados.DataPagamento,
                    DataCancelamento = result.Dados.DataCancelamento,
                    Valor = result.Dados.Valor,
                    ValorMora = result.Dados.ValorMora,
                    ValorJuros = result.Dados.ValorJuros,
                    ValorDesconto = result.Dados.ValorDesconto,
                    ValorPago = result.Dados.ValorPago,
                    Situacao = result.Dados.Situacao,
                    NumeroParcela = result.Dados.NumeroParcela,
                    TipoFinanceiro = result.Dados.TipoFinanceiro,
                    Observacao = result.Dados.Observacao,
                    TipoCredito = result.Dados.TipoCredito
                };
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Financeiro não encontrado.", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}
