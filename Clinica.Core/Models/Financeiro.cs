using Clinica.Core.Enums;

namespace Clinica.Core.Models;

public class Financeiro
{
    public long Id { get; set; }
    public long ContratoId { get; set; }
    public Contrato Contrato { get; set; } = new();
    public DateTime DataEmissao { get; set; }
    public DateTime DataVencimento { get; set; }
    public DateTime DataPagamento { get; set; }
    public DateTime DataCancelamento { get; set; }
    public decimal Valor { get; set; }
    public decimal ValorMora { get; set; }
    public decimal ValorJuros { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal ValorPago { get; set; }
    public ESituacaoFinanceiro Situacao { get; set; }
    public int NumeroParcela { get; set; }
    public ETipoFinanceiro TipoFinanceiro { get; set; }
    public string? Observacao { get; set; }
}
