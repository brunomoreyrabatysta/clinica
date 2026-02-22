using Clinica.Core.Handlers;
using Clinica.Core.Models;
using Clinica.Core.Requests.Cidades;
using Clinica.Core.Requests.Pacientes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Clinica.Web.Pages.Pacientes;

public partial class EditarPacientePage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public AlterarPacienteRequest InputModel { get; set; } = new();
    public List<Cidade> Cidades { get; set; } = [];
    #endregion

    #region Parameter
    [Parameter]
    public string Id { get; set; } = string.Empty;
    #endregion

    #region Services
    [Inject]
    public IPacienteHandler Handler { get; set; } = null!;

    [Inject]
    public ICidadeHandler CidadeHandler { get; set; } = null!;

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
            await CarregarPacientePorIdAsync();
            await CarregarCidadeAsync();
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
                Snackbar.Add("Paciente alterado com sucesso!", Severity.Success);
                NavigationManager.NavigateTo("/paciente");
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Não foi possível alterar o paciente.", Severity.Error);
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
    private async Task CarregarCidadeAsync()
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

    private async Task CarregarPacientePorIdAsync()
    {
        try
        {
            var id = int.Parse(Id);
            var request = new ListarPacientePorIdRequest
            {
                Id = id
            };

            var result = await Handler.ListarPacientePorIdAsync(request);
            if (result is { Sucesso: true, Dados: not null })
            {
                InputModel = new AlterarPacienteRequest
                {
                    Id = result.Dados.Id,
                    Nome = result.Dados.Nome,
                    CPF = result.Dados.CPF,
                    RG = result.Dados.RG,
                    DataEmisssaoRG = result.Dados.DataEmisssaoRG,
                    UFEmissaoRG = result.Dados.UFEmissaoRG,
                    Endereco = result.Dados.Endereco,
                    Complemento = result.Dados.Complemento,
                    Numero = result.Dados.Numero,
                    Bairro = result.Dados.Bairro,
                    CidadeId = result.Dados.CidadeId,
                    CEP = result.Dados.CEP,
                    Naturalidade = result.Dados.Naturalidade,
                    Nacionalidade = result.Dados.Nacionalidade,
                    Sexo = result.Dados.Sexo,
                    DataNascimento = result.Dados.DataNascimento,
                    NumeroTelefone = result.Dados.NumeroTelefone,
                    Email = result.Dados.Email
                };
            }
            else
            {
                Snackbar.Add(result.Mensagem ?? "Paciente não encontrado.", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
}
