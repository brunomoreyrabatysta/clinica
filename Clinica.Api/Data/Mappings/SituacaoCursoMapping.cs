using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinica.Core.Models;

namespace Clinica.Api.Data.Mappings;

public class SituacaoCursoMapping : IEntityTypeConfiguration<SituacaoCurso>
{
    public void Configure(EntityTypeBuilder<SituacaoCurso> builder)
    {
        builder.ToTable("SituacaoCurso");

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
