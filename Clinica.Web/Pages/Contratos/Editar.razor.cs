using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Requests.Vinculos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Contratos;

public partial class EditarContratoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public AlterarContratoRequest InputModel { get; set; } = new();
    public List<Paciente> Pacientes { get; set; } = [];
    public List<Responsavel> Responsaveis { get; set; } = [];
    public List<Vinculo> Vinculos { get; set; } = [];
    #endregion

    #region Parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public IContratoHandler Handler { get; set; } = null!;

    [Inject]
    public IPacienteHandler PacienteHandler { get; set; } = null!;

    [Inject]
    public IResponsavelHandler ResponsavelHandler { get; set; } = null!;

    [Inject]
    public IVinculoHandler VinculoHandler { get; set; } = null!;

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
            await CarregarPacientesAsync();
            await CarregarResponsaveisAsync();
            await CarregarVinculosAsync();
            await CarregarContratoPorIdAsync();
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
                Snackbar.Add("Contrato alterado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/contrato");
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Não foi possível alterar o contrato.", Severity.Error);
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

    protected void CalculoValorContratoLiquido()
    {
        if (InputModel.ValorDesconto > InputModel.ValorContrato)
        {
            Snackbar.Add("O valor do desconto não pode ser maior que o valor do contrato.", Severity.Warning);
            InputModel.ValorDesconto = 0;
        }

        if (InputModel.ValorContrato > 0 && InputModel.ValorDesconto > 0)
        {
            InputModel.ValorContratoLiquido = InputModel.ValorContrato - InputModel.ValorDesconto;
        }
        else
            InputModel.ValorContratoLiquido = InputModel.ValorContrato;
    }
    #endregion

    #region Private Methods
    private async Task CarregarPacientesAsync()
    {
        try
        {
            var request = new ListarTodosPacientesRequest();
            var result = await PacienteHandler.ListarTodosPacientesAsync(request);
            if (result.Sucesso)
            {
                Pacientes = result.Dados ?? [];
                InputModel.PacienteId = Pacientes.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os pacientes.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task CarregarResponsaveisAsync()
    {
        try
        {
            var request = new ListarTodosResponsaveisRequest();
            var result = await ResponsavelHandler.ListarTodosResponsaveisAsync(request);
            if (result.Sucesso)
            {
                Responsaveis = result.Dados ?? [];
                InputModel.ResponsavelId = Responsaveis.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os responsáveis.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task CarregarVinculosAsync()
    {
        try
        {
            var request = new ListarTodosVinculosRequest();
            var result = await VinculoHandler.ListarTodosVinculosAsync(request);
            if (result.Sucesso)
            {
                Vinculos = result.Dados ?? [];
                InputModel.VinculoId = Vinculos.FirstOrDefault()?.Id ?? 0;
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao carregar os vínculos.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    private async Task CarregarContratoPorIdAsync()
    {
        try
        {
            var id = int.Parse(Id);
            var request = new ListarContratoPorIdRequest
            {
                Id = id
            };

            var result = await Handler.ListarContratoPorIdAsync(request);
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarContratoRequest
                {
                    Id = result.Dados.Id,
                    PacienteId = result.Dados.PacienteId,
                    ResponsavelId = result.Dados.ResponsavelId,
                    VinculoId = result.Dados.VinculoId,
                    Situacao = result.Dados.Situacao,
                    DataEmissao = result.Dados.DataEmissao,
                    DataInicio = result.Dados.DataInicio,
                    DataTermino = result.Dados.DataTermino,
                    DataCancelamento = result.Dados.DataCancelamento,
                    Periodo = result.Dados.Periodo,
                    ValorContrato = result.Dados.ValorContrato,
                    ValorDesconto = result.Dados.ValorDesconto,
                    ValorContratoLiquido = result.Dados.ValorContratoLiquido,
                    NumeroParcela = result.Dados.NumeroParcela,
                    ValorEntrada = result.Dados.ValorEntrada,
                    ValorParcela = result.Dados.ValorParcela,
                    DataEntrada = result.Dados.DataEntrada,
                    DiaVencimentoDemaisParcelas = result.Dados.DiaVencimentoDemaisParcelas,
                    ValorProfissionalEquipe = result.Dados.ValorProfissionalEquipe,
                    ValorProfissionalEquipe_Hora = result.Dados.ValorProfissionalEquipe_Hora,
                    ValorTerapeutico = result.Dados.ValorTerapeutico,
                    Observacao = result.Dados.Observacao,
                    ValorCreditoMensal = result.Dados.ValorCreditoMensal
                };
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Contrato não encontrado.", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}
