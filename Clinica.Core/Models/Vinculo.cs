using System;
using System.Collections.Generic;
using System.Text;

namespace Clinica.Core.Models;

public class Vinculo
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public List<Contrato> Contratos { get; set; } = new();
}
