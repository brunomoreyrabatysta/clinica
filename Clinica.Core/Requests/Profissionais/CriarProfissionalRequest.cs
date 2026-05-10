using Clinica.Core.Enums;
using Clinica.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Profissionais;

public class CriarProfissionalRequest : BaseRequest
{
    [Required(ErrorMessage = "O nome não foi preenchido!")]
    public string Nome { get; set; } = string.Empty;
    public string? CPF { get; set; }
    [Required(ErrorMessage = "O tipo do profissional não foi preenchido!")]
    public ETipoProfissional Tipo { get; set; }
    public string? NumeroTelefone { get; set; }
    public string? Email { get; set; }
    public string? NumeroRegistro { get; set; }
    public long? UnidadeFederativaId { get; set; }
    public UnidadeFederativa? UnidadeFederativa { get; set; }
    public string? Conselho { get; set; }
    public ESituacao Situacao { get; set; } = ESituacao.Ativo;

}
