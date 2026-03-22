using Clinica.Core.Enums;

namespace Clinica.Core.Models;

public class Contrato
{
    public long Id { get; set; }
    public long PacienteId { get; set; }
    public Paciente Paciente { get; set; } = new ();
    public long ResponsavelId { get; set; }
    public Responsavel Responsavel { get; set; } = new ();
    public long VinculoId { get; set; }
    public Vinculo Vinculo { get; set; } = new ();
    public ESituacao Situacao { get; set; }
    public DateTime DataEmissao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataTermino { get; set; }
    public DateTime DataCancelamento { get; set; }
    public int Periodo { get; set; }
    public decimal ValorContrato { get; set; }
    public decimal ValorDesconto { get; set; }
    public decimal ValorContratoLiquido { get; set; }
    public int NumeroParcela { get; set; }
    public decimal ValorEntrada { get; set; }
    public decimal ValorParcela { get; set; }
    public DateTime DataEntrada { get; set; }
    public int DiaVencimentoDemaisParcelas { get; set; }
    public decimal ValorProfissionalEquipe { get; set; }
    public decimal ValorProfissionalEquipe_Hora { get; set; }
    public decimal ValorTerapeutico { get; set; }
    public string? Observacao { get; set; }

    public List<Financeiro> Financeiros { get; set; } = new();
}