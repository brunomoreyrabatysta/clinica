using Clinica.Core.Enums;
using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Contratos;
using Clinica.Core.Requests.Pacientes;
using Clinica.Core.Requests.Responsaveis;
using Clinica.Core.Requests.Vinculos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Contratos;

public partial class CriarContratoPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarContratoRequest InputModel { get; set; } = new();

    public List<Paciente> Pacientes { get; set; } = [];
    public List<Responsavel> Responsaveis { get; set; } = [];
    public List<Vinculo> Vinculos { get; set; } = [];

    protected bool isNaoCancelado = true;
    protected bool isNaoTemEntrada = true;
    protected bool isNaoTemParcela = true;
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

    #region Override
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            await CarregarPacientesAsync();
            await CarregarResponsaveisAsync();
            await CarregarVinculosAsync();
            InicializarCampos();
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
            if (!ValidarCampos())
                return;

            var result = await Handler.CriarAsync(InputModel);
            if (result.Sucesso)
            {
                Snackbar.Add(result.Mensagem ?? "Contrato criado com sucesso.", Severity.Success);
                NavigationManager.NavigateTo("/contrato");
            }
            else
                Snackbar.Add(result.Mensagem ?? "Falha ao criar o contrato.", Severity.Error);
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

    protected void AlterarSituacao()
    {
        isNaoCancelado = InputModel.Situacao != ESituacaoContrato.Cancelado;


        if (isNaoCancelado)
            InputModel.DataCancelamento = null;
    }

    protected void VerificarEntrada()
    {
        isNaoTemEntrada = InputModel.ValorEntrada == 0;

        if (isNaoTemEntrada)
            InputModel.DataEntrada = null;
    }

    protected void VerificarParcela()
    {
        isNaoTemParcela = InputModel.NumeroParcela == 0;

        if (isNaoTemParcela)
        {
            InputModel.ValorParcela = 0;
            InputModel.DiaVencimentoDemaisParcelas = 0;
        }
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

    private void InicializarCampos()
    {
        InputModel = new CriarContratoRequest
        {
            PacienteId = Pacientes.FirstOrDefault()?.Id ?? 0,
            ResponsavelId = Responsaveis.FirstOrDefault()?.Id ?? 0,
            VinculoId = Vinculos.FirstOrDefault()?.Id ?? 0,
            Situacao = ESituacaoContrato.Aberto,
            DataEmissao = DateTime.Now.Date,
            DataInicio = DateTime.Now.Date,
            DataTermino = DateTime.Now.Date.AddMonths(1),
            Periodo = 1,
            ValorContrato = 0,
            ValorDesconto = 0,
            ValorContratoLiquido = 0,
            ValorEntrada = 0,
            NumeroParcela = 0,
            ValorProfissionalEquipe = 150,
            ValorProfissionalEquipe_Hora = 50,
            ValorTerapeutico = 100,
            ValorDiaria = 120,
            PercentualMora = 1,
            PercentualJuros = 2,
            PercentualMultaContratual = 25
        };

        isNaoCancelado = true;
        isNaoTemEntrada = true;
        isNaoTemParcela = true;
    }

    private bool ValidarCampos()
    {
        if (InputModel.PacienteId <= 0)
        {
            Snackbar.Add("Selecione um paciente válido.", Severity.Warning);
            return false;
        }

        if (InputModel.ResponsavelId <= 0)
        {
            Snackbar.Add("Selecione um responsável válido.", Severity.Warning);
            return false;
        }

        if (InputModel.VinculoId <= 0)
        {
            Snackbar.Add("Selecione um vínculo válido.", Severity.Warning);
            return false;
        }

        if (InputModel.Situacao <= 0)
        {
            Snackbar.Add("Selecione uma situação válida.", Severity.Warning);
            return false;
        }

        if (InputModel.DataEmissao == default)
        {
            Snackbar.Add("A data de emissão é obrigatória.", Severity.Warning);
            return false;
        }

        if (InputModel.DataInicio == default)
        {
            Snackbar.Add("A data de início é obrigatória.", Severity.Warning);
            return false;
        }

        if (InputModel.DataTermino == default)
        {
            Snackbar.Add("A data de início é obrigatória.", Severity.Warning);
            return false;
        }

        if (InputModel.DataInicio > InputModel.DataTermino)
        {
            Snackbar.Add("A data de início não pode ser posterior à data de término.", Severity.Warning);
            return false;
        }

        if ((InputModel.Situacao == ESituacaoContrato.Cancelado) && (!InputModel.DataCancelamento.HasValue))
        {
            Snackbar.Add("A data de cancelamento é obrigatória para contratos cancelados.", Severity.Warning);
            return false;
        }

        if ((InputModel.Situacao != ESituacaoContrato.Cancelado) && (InputModel.DataCancelamento.HasValue))
        {
            Snackbar.Add("A data de cancelamento só pode ser preenchida para contratos cancelados.", Severity.Warning);
            return false;
        }

        if (InputModel.DataCancelamento.HasValue && InputModel.DataCancelamento.Value < InputModel.DataInicio)
        {
            Snackbar.Add("A data de cancelamento não pode ser anterior à data de início.", Severity.Warning);
            return false;
        }

        if (InputModel.Periodo <= 0 || InputModel.Periodo > 12)
        {
            Snackbar.Add("O período deve estar entre 1 e 12 meses.", Severity.Warning);
            return false;
        }

        if (InputModel.ValorContrato <= 0)
        {
            Snackbar.Add("O valor do contrato deve ser maior que zero.", Severity.Warning);
            return false;
        }
        if (InputModel.ValorDesconto < 0)
        {
            Snackbar.Add("O valor do desconto não pode ser negativo.", Severity.Warning);
            return false;
        }
        if (InputModel.ValorDesconto > InputModel.ValorContrato)
        {
            Snackbar.Add("O valor do desconto não pode ser maior que o valor do contrato.", Severity.Warning);
            return false;
        }

        if (InputModel.ValorContratoLiquido < 0)
        {
            Snackbar.Add("O valor líquido do contrato não pode ser negativo.", Severity.Warning);
            return false;
        }

        if (InputModel.ValorContratoLiquido > InputModel.ValorContrato)
        {
            Snackbar.Add("O valor líquido do contrato não pode ser maior que o valor do contrato.", Severity.Warning);
            return false;
        }

        if (InputModel.ValorDesconto > 0 && InputModel.ValorContratoLiquido <= 0)
        {
            Snackbar.Add("O valor líquido do contrato deve ser maior que zero quando houver desconto.", Severity.Warning);
            return false;
        }

        if ((InputModel.ValorContrato - InputModel.ValorDesconto) != InputModel.ValorContratoLiquido)
        {
            Snackbar.Add("O valor líquido do contrato deve ser igual ao valor do contrato menos o valor do desconto.", Severity.Warning);
            return false;
        }

        if (InputModel.ValorEntrada < 0)
        {
            Snackbar.Add("O valor de entrada não pode ser negativo.", Severity.Warning);
            return false;
        }

        if (InputModel.ValorEntrada > InputModel.ValorContratoLiquido)
        {
            Snackbar.Add("O valor de entrada não pode ser maior que o valor líquido do contrato.", Severity.Warning);
            return false;
        }

        if (InputModel.ValorEntrada > 0 && InputModel.DataEntrada == default)
        {
            Snackbar.Add("A data de entrada é obrigatória quando houver valor de entrada.", Severity.Warning);
            return false;
        }

        if (InputModel.ValorEntrada > 0 && !InputModel.DataEntrada.HasValue) 
        { 
            Snackbar.Add("A data de entrada é obrigatória quando houver valor de entrada.", Severity.Warning);
            return false;
        }

        if (InputModel.DataEntrada.HasValue)
        {
            if (InputModel.DataEntrada < InputModel.DataEmissao)
            {
                Snackbar.Add("A data de entrada não pode ser anterior à data de emissão.", Severity.Warning);
                return false;
            }
            if (InputModel.DataEntrada > InputModel.DataTermino)
            {
                Snackbar.Add("A data de entrada não pode ser posterior à data de término.", Severity.Warning);
                return false;
            }
        }

        if (InputModel.NumeroParcela < 0)
        {
            Snackbar.Add("O número de parcelas não pode ser negativo.", Severity.Warning);
            return false;
        }

        if (InputModel.NumeroParcela < 0 || InputModel.NumeroParcela > 12)
        {
            Snackbar.Add("O número de parcelas deve estar entre 0 e 12 parcelas.", Severity.Warning);
            return false;
        }


        return true;
    }
    #endregion
}
