using Clinica.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clinica.Core.Models;

public class Vinculo
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    [NotMapped]
    public List<Contrato> Contratos { get; set; } = new();

    public ESituacao Situacao { get; set; } = ESituacao.Ativo;
}
