using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Core.Models;

public class Cidade
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public long UnidadeFederativaId { get; set; }
    [NotMapped]
    public UnidadeFederativa UnidadeFederativa { get; set; } = null!;

    [NotMapped]
    public ICollection<Paciente> Pacientes { get; } = new List<Paciente>();
    [NotMapped]
    public ICollection<Responsavel> Responsaveis { get; } = new List<Responsavel>();
}
