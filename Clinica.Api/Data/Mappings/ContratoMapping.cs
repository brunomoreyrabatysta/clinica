using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class ContratoMapping : IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {
        builder.ToTable("Contrato");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.PacienteId)
            .HasColumnType("INT");
        builder.Property(x => x.ResponsavelId)
            .HasColumnType("INT");
        builder.Property(x => x.VinculoId)
            .HasColumnType("INT");
        builder.Property(x => x.Situacao)
            .HasColumnType("CHAR")
            .HasMaxLength(1);
        builder.Property(x => x.DataEmissao)
            .HasColumnType("DATE");
        builder.Property(x => x.DataInicio)
            .HasColumnType("DATE");
        builder.Property(x => x.DataTermino)
            .HasColumnType("DATE");
        builder.Property(x => x.DataCancelamento)
            .HasColumnType("DATE");
        builder.Property(x => x.Periodo)
            .HasColumnType("INT");
        builder.Property(x => x.ValorContrato)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.NumeroParcela)
            .HasColumnType("INT");
        builder.Property(x => x.ValorEntrada)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorParcela)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.DataEntrada)
            .HasColumnType("DATE");
        builder.Property(x => x.DiaVencimentoDemaisParcelas)
            .HasColumnType("INT");
        builder.Property(x => x.ValorProfissionalEquipe)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorProfissionalEquipe_Hora)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorTerapeutico)
            .HasColumnType("NUMERIC(15,2)");

        builder.HasOne(p => p.Paciente)
                .WithMany(c => c.Contratos)
                .HasForeignKey(p => p.PacienteId);

        builder.HasOne(p => p.Responsavel)
                .WithMany(c => c.Contratos)
                .HasForeignKey(p => p.ResponsavelId);

        builder.HasOne(p => p.Vinculo)
                .WithMany(c => c.Contratos)
                .HasForeignKey(p => p.VinculoId);
    }
}
