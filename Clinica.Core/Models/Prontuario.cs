using Clinica.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Core.Models;

public class Prontuario
{
    public long Id { get; set; }
    public DateTime? DataProntuario { get; set; }
    public ECobranca Cobranca { get; set; }
    public long ProfissionalId { get; set; }
    [NotMapped]
    public Profissional Profissional { get; set; } = null!;
    public string Descricao { get; set; } = string.Empty;
    public long ContratoId { get; set; }

    [NotMapped]
    public Contrato Contrato { get; set; } = null!;
    public string TempoAtendimento { get; set; } = string.Empty;
}
