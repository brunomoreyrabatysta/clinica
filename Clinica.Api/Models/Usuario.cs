using Microsoft.AspNetCore.Identity;

namespace Clinica.Api.Models;

public class Usuario : IdentityUser<long>
{
    public List<IdentityRole<long>>? Perfis { get; set; }
}
