using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Clinica.Api.Models;
using Clinica.Core.Models;
using System.Reflection;

namespace Clinica.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<
        Usuario,
        IdentityRole<long>,
        long,
        IdentityUserClaim<long>,
        IdentityUserRole<long>,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>
        >
        (options)
{
    public DbSet<Cidade> Cidades { get; set; } = null!;
    public DbSet<UnidadeFederativa> UnidadesFederativa { get; set; } = null!;
    public DbSet<Paciente> Pacientes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
