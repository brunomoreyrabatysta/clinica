using System.Text.Json.Serialization;

namespace Clinica.Core.Responses;

public class Response<TDado>
{

    private readonly int _codigo;

    [JsonConstructor]
    public Response() => _codigo = Configuracao.PadraoCodigoSituacao;

    public Response(TDado? dados, int codigo = Configuracao.PadraoCodigoSituacao, string? mensagem = null)
    {
        Dados = dados;
        Mensagem = mensagem;
        _codigo = codigo;
    }

    public TDado? Dados { get; set; }
    public string? Mensagem { get; set; } = string.Empty;

    [JsonIgnore]
    public bool Sucesso => _codigo is >= 200 and <= 299;
}
