using Clinica.Core.Enums;
using Clinica.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Prontuarios;

public class CriarProntuarioRequest : BaseRequest
{
    [Required(ErrorMessage = "A data do prontuário não foi preenchida!")]
    public DateTime? DataProntuario { get; set; }
    public ECobranca Cobranca { get; set; }
    
    [Required(ErrorMessage = "O profissional não foi preenchido!")]
    public long ProfissionalId { get; set; }    

    [Required(ErrorMessage = "A descrição não foi preenchida!")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O contrato não foi preenchido!")]
    public long ContratoId { get; set; }
    
    public string TempoAtendimento { get; set; } = string.Empty;
}
