using Clinica.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Paciente;

public class AlterarPacienteRequest : BaseRequest
{
    [Required(ErrorMessage = "O código do paciente não foi preenchido!")]
    public long Id { get; set; }
    [Required(ErrorMessage = "O nome não foi preenchido!")]
    public string Nome { get; set; } = string.Empty;
    [Required(ErrorMessage = "O CPF não foi preenchido!")]
    public string CPF { get; set; } = string.Empty;
    public string? RG { get; set; }
    public DateTime? DataEmisssaoRG { get; set; }
    public string? UFEmissaoRG { get; set; }
    public string? Endereco { get; set; }
    public string? Complemento { get; set; }
    public string? Numero { get; set; }
    public string? Bairro { get; set; }
    public int? CidadeId { get; set; }
    public string? CEP { get; set; }
    public string? Naturalidade { get; set; }
    public string? Nacionalidade { get; set; }
    public ESexo? Sexo { get; set; }

    [Required(ErrorMessage = "A data de nascimento não foi preenchida!")]
    public DateTime DataNascimento { get; set; }
    public string? NumeroTelefone { get; set; }
    public string? Email { get; set; }
}
