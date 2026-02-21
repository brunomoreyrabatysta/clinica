using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Clinica.Core.Models;

namespace Clinica.Api.Data.Mappings;

public class CursoMapping : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("Curso");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Titulo)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);
        builder.Property(x => x.Descricao)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);
        builder.Property(x => x.CodigoCredencial)
            .HasColumnType("VARCHAR")
            .HasMaxLength(40);
        builder.Property(x => x.OrganizacaoEmissora)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);
        builder.Property(x => x.DataEmissao)
            .IsRequired()
            .HasColumnType("DATE");
        builder.Property(x => x.DataInicio)
            .HasColumnType("DATE");
        builder.Property(x => x.DataTermino)
            .HasColumnType("DATE");
        builder.Property(x => x.DataExpiracao)
            .HasColumnType("DATE");
        builder.Property(x => x.UrlCredencial)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(500);
        builder.Property(x => x.Competencia)
            .HasColumnType("VARCHAR")
            .HasMaxLength(500);
        builder.Property(x => x.CargaHoraria)
            .IsRequired()
            .HasColumnType("DECIMAL(5,3)");
    }
}
