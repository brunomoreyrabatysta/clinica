namespace Clinica.Core.Models;

public class Cidade
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public long UnidadeFederativaId { get; set; }
    public UnidadeFederativa UnidadeFederativa { get; set; } = null!;
}
