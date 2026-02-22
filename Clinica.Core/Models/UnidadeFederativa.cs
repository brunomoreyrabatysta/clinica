namespace Clinica.Core.Models;

public class UnidadeFederativa
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Sigla { get; set; } = string.Empty;

    public ICollection<Cidade> Cidades { get; } = new List<Cidade>();
}
