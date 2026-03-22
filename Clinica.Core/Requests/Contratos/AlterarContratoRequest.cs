using Clinica.Core.Enums;
using Clinica.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Contratos;

public class AlterarContratoRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do contrato não foi preenchido!")]
    public long Id { get; set; }

    [Required(ErrorMessage = "O paciente não foi preenchido!")]
    public long PacienteId { get; set; }
    public Paciente Paciente { get; set; } = new();

    [Required(ErrorMessage = "O responsável não foi preenchido!")]
    public long ResponsavelId { get; set; }
    public Responsavel Responsavel { get; set; } = new();
    
    [Required(ErrorMessage = "O vínculo não foi preenchido!")]
    public long VinculoId { get; set; }
    public Vinculo Vinculo { get; set; } = new();

    [Required(ErrorMessage = "A situação não foi preenchida!")]
    public ESituacao Situacao { get; set; }
    
    [Required(ErrorMessage = "A data de emissão não foi preenchida!")]
    public DateTime DataEmissao { get; set; }
    
    [Required(ErrorMessage = "A data de início não foi preenchida!")]
    public DateTime DataInicio { get; set; }
    
    [Required(ErrorMessage = "A data de término não foi preenchida!")]
    public DateTime DataTermino { get; set; }
    public DateTime DataCancelamento { get; set; }

    [Required(ErrorMessage = "O período não foi preenchido!")]
    public int Periodo { get; set; }
    
    [Required(ErrorMessage = "O valor do contrato não foi preenchido!")]
    public decimal ValorContrato { get; set; }
    public decimal ValorDesconto { get; set; }

    [Required(ErrorMessage = "O valor líquido do contrato não foi preenchido!")]
    public decimal ValorContratoLiquido { get; set; }

    [Required(ErrorMessage = "O número de parcelas não foi preenchido!")]
    public int NumeroParcela { get; set; }
    public decimal ValorEntrada { get; set; }
    public decimal ValorParcela { get; set; }
    public DateTime DataEntrada { get; set; }

    [Required(ErrorMessage = "O dia de vencimento das demais parcelas não foi preenchido!")]
    public int DiaVencimentoDemaisParcelas { get; set; }

    [Required(ErrorMessage = "O valor do profissional da equipe não foi preenchido!")]
    public decimal ValorProfissionalEquipe { get; set; }
    
    [Required(ErrorMessage = "O valor da hora do profissional da equipe não foi preenchido!")]
    public decimal ValorProfissionalEquipe_Hora { get; set; }
    
    [Required(ErrorMessage = "O valor terapêutico não foi preenchido!")]
    public decimal ValorTerapeutico { get; set; }
    public string? Observacao { get; set; }
}