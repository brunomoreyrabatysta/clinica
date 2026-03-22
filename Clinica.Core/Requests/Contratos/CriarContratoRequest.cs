using Clinica.Core.Enums;
using Clinica.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Contratos;

public class CriarContratoRequest : BaseRequest
{
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
    public double ValorContrato { get; set; }

    [Required(ErrorMessage = "O número de parcelas não foi preenchido!")]
    public int NumeroParcela { get; set; }
    public double ValorEntrada { get; set; }
    public double ValorParcela { get; set; }
    public DateTime DataEntrada { get; set; }
    public int DiaVencimentoDemaisParcelas { get; set; }
    public double ValorProfissionalEquipe { get; set; }
    public double ValorProfissionalEquipe_Hora { get; set; }
    public double ValorTerapeutico { get; set; }
}
