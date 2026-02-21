using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class TipoCursoMapping : IEntityTypeConfiguration<TipoCurso>
{
    public void Configure(EntityTypeBuilder<TipoCurso> builder)
    {
        builder.ToTable("TipoCurso");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        builder.Property(x => x.Descricao)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);
    }
}
