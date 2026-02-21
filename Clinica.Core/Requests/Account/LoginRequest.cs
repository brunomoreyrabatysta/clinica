using System.ComponentModel.DataAnnotations;

namespace Clinica.Core.Requests.Account;

public class LoginRequest
{
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha inválido")]
    public string Password { get; set; } = string.Empty;
}
