using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Curso;

public class ListarCursoPorTipoCursoRequest : PaginacaoRequest
{
    [Required(ErrorMessage = "O tipo de curso não foi preenchido!")]
    public int TipoCursoId { get; set; }
}

