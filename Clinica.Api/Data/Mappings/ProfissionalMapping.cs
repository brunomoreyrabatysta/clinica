using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class ProfissionalMapping : IEntityTypeConfiguration<Profissional>
{
    public void Configure(EntityTypeBuilder<Profissional> builder)
    {
        builder.ToTable("Profissional");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(300);
        builder.Property(x => x.CPF)
            .HasColumnType("VARCHAR")
            .HasMaxLength(14);
        builder.Property(x => x.Tipo)
            .HasColumnType("INT");
        builder.Property(x => x.NumeroTelefone)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);
        builder.Property(x => x.Email)
            .HasColumnType("VARCHAR")
            .HasMaxLength(300);
        builder.Property(x => x.NumeroRegistro)
            .HasColumnType("VARCHAR")
            .HasMaxLength(30);
        builder.Property(x => x.UnidadeFederativaId)
            .HasColumnType("INT");
        builder.Property(x => x.Conselho)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
        builder.Property(x => x.Situacao)
            .HasColumnType("INT");

        builder.HasOne(p => p.UnidadeFederativa)
                .WithMany(c => c.Profissionais)
                .HasForeignKey(p => p.UnidadeFederativaId);
    }
}