using Clinica.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class CidadeMapping : IEntityTypeConfiguration<Cidade>
{
    public void Configure(EntityTypeBuilder<Cidade> builder)
    {
        builder.ToTable("Cidade");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(300);
        builder.Property(x => x.UnidadeFederativaId)
            .IsRequired()
            .HasColumnType("INT");
        
        builder.HasMany(c => c.Pacientes)
            .WithOne(p => p.Cidade)
            .HasForeignKey(p => p.CidadeId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.UnidadeFederativa)
                        .WithMany(uf => uf.Cidades)
                        .HasForeignKey(c => c.UnidadeFederativaId);        
    }
}
