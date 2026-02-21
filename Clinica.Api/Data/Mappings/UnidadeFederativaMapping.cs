using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class UnidadeFederativaMapping : IEntityTypeConfiguration<UnidadeFederativa>
{
    public void Configure(EntityTypeBuilder<UnidadeFederativa> builder)
    {
        builder.ToTable("UnidadeFedrativa");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(300);
        builder.Property(x => x.Sigla)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(2);
    }
}
