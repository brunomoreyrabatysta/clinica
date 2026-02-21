using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Curso;

public class CriarCursoRequest : BaseRequest
{
    [Required(ErrorMessage = "O título não foi preenchido!")]
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? CodigoCredencial { get; set; }

    [Required(ErrorMessage = "A unidade organizadora emissora do certificado do curso não foi preenchida!")]
    public string OrganizacaoEmissora { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de emissão do certificado não foi preenchida!")]
    public DateTime? DataEmissao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
    public DateTime? DataExpiracao { get; set; }
    public string UrlCredencial { get; set; } = string.Empty;
    public string? Competencia { get; set; }

    [Required(ErrorMessage = "A carga horária não foi preenchida!")]
    public decimal CargaHoraria { get; set; }

    [Required(ErrorMessage = "O tipo de curso não foi preenchido!")]
    public int TipoCursoId { get; set; }

    [Required(ErrorMessage = "A situação de curso não foi preenchidda!")]
    public int SituacaoCursoId { get; set; }
}
