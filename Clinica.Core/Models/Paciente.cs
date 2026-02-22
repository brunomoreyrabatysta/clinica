using Clinica.Core.Enums;

namespace Clinica.Core.Models;

public class Paciente
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string? RG { get; set; }
    public DateTime? DataEmisssaoRG { get; set; }
    public string? UFEmissaoRG { get; set; }
    public string? Endereco { get; set; }
    public string? Complemento { get; set; }
    public string? Numero { get; set; }
    public string? Bairro { get; set; }
    public Cidade? Cidade { get; set; }
    public long? CidadeId { get; set; }
    public string? CEP { get; set; }
    public string? Naturalidade { get; set; }
    public string? Nacionalidade { get; set; }
    public ESexo? Sexo { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? NumeroTelefone { get; set; }
    public string? Email { get; set; }
}
