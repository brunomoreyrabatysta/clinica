using Clinica.Core.Enums;
using Clinica.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Financeiros;

public class CriarFinanceiroRequest : BaseRequest
{
    [Required(ErrorMessage = "O contrato não foi preenchido!")]
    public long ContratoId { get; set; }
    public Contrato Contrato { get; set; } = new();

    [Required(ErrorMessage = "A data de emissão não foi preenchida!")]
    public DateTime DataEmissao { get; set; }

    [Required(ErrorMessage = "A data de vencimento não foi preenchida!")]
    public DateTime DataVencimento { get; set; }
    public DateTime DataPagamento { get; set; }
    public DateTime DataCancelamento { get; set; }

    [Required(ErrorMessage = "O valor do financeiro não foi preenchido!")]
    public decimal Valor { get; set; }
    public decimal ValorMora { get; set; }
    public decimal ValorJuros { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal ValorPago { get; set; }

    [Required(ErrorMessage = "A situação financeiro não foi preenchida!")]
    public ESituacaoFinanceiro Situacao { get; set; }
    public int NumeroParcela { get; set; }

    [Required(ErrorMessage = "O tipo do financeiro não foi preenchido!")]
    public ETipoFinanceiro TipoFinanceiro { get; set; }
    public string? Observacao { get; set; }
}
