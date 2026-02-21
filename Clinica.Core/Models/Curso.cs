namespace Clinica.Core.Models;

public class Curso
{
    public long Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string? CodigoCredencial { get; set; }
    public string OrganizacaoEmissora { get; set; } = string.Empty;
    public DateTime? DataEmissao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
    public DateTime? DataExpiracao { get; set; }
    public string UrlCredencial { get; set; } = string.Empty;
    public string? Competencia { get; set; }
    public decimal CargaHoraria { get; set; }
    public int TipoCursoId { get; set; }
    public SituacaoCurso TipoCurso { get; set; } = null!;
    public int SituacaoCursoId { get; set; }
    public SituacaoCurso SituacaoCurso { get; set; } = null!;
}
