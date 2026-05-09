using Clinica.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Core.Models;

public class Profissional
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CPF { get; set; }
    public ETipoProfissional Tipo { get; set; }
    public string? NumeroTelefone { get; set; }
    public string? Email { get; set; }
    public string? NumeroRegistro { get; set; }
    public int? UnidadeFederativaId { get; set; }
    
    [NotMapped]
    public UnidadeFederativa? UnidadeFederativa { get; set; }
    public string? Conselho { get; set; }

    public ESituacao Situacao { get; set; } = ESituacao.Ativo;
}
