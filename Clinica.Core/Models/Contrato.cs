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
    public double ValorContrato { get; set; }
    public int NumeroParcela { get; set; }
    public double ValorEntrada { get; set; }
    public double ValorParcela { get; set; }
    public DateTime DataEntrada { get; set; }
    public int DiaVencimentoDemaisParcelas { get; set; }
    public double ValorProfissionalEquipe { get; set; }
    public double ValorProfissionalEquipe_Hora { get; set; }
    public double ValorTerapeutico { get; set; }
}
