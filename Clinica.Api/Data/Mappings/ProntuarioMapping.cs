using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class ProntuarioMapping : IEntityTypeConfiguration<Prontuario>
{
    public void Configure(EntityTypeBuilder<Prontuario> builder)
    {
        builder.ToTable("Prontuario");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.DataProntuario)
            .IsRequired()
            .HasColumnType("DATE");
        builder.Property(x => x.Cobranca)
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.ProfissionalId)
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(8000);
        builder.Property(x => x.ContratoId)
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.TempoAtendimento)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(10);
    }
}
