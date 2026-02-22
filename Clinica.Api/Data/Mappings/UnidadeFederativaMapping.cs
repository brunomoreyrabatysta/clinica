using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class UnidadeFederativaMapping : IEntityTypeConfiguration<UnidadeFederativa>
{
    public void Configure(EntityTypeBuilder<UnidadeFederativa> builder)
    {
        builder.ToTable("UnidadeFederativa");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(300);
        builder.Property(x => x.Sigla)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(2);
        
        builder.HasMany(uf => uf.Cidades)
            .WithOne(c => c.UnidadeFederativa)
            .HasForeignKey(c => c.UnidadeFederativaId)
            .HasPrincipalKey(uf => uf.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
