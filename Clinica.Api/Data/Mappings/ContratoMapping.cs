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
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.ResponsavelId)
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.VinculoId)
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.Situacao)
            .IsRequired()
            .HasColumnType("CHAR")
            .HasMaxLength(1);
        builder.Property(x => x.DataEmissao)
            .IsRequired()
            .HasColumnType("DATE");
        builder.Property(x => x.DataInicio)
            .IsRequired()
            .HasColumnType("DATE");
        builder.Property(x => x.DataTermino)
            .IsRequired()
            .HasColumnType("DATE");
        builder.Property(x => x.DataCancelamento)
            .HasColumnType("DATE");
        builder.Property(x => x.Periodo)
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.ValorContrato)
            .IsRequired()
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorDesconto)            
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorContratoLiquido)
            .IsRequired()
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.NumeroParcela)
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.ValorEntrada)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorParcela)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.DataEntrada)
            .HasColumnType("DATE");
        builder.Property(x => x.DiaVencimentoDemaisParcelas)
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.ValorProfissionalEquipe)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorProfissionalEquipe_Hora)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorTerapeutico)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.Observacao)            
            .HasColumnType("VARCHAR")
            .HasMaxLength(8000);

        builder.HasOne(c => c.Paciente)
                .WithMany(p => p.Contratos)
                .HasForeignKey(c => c.PacienteId);

        builder.HasOne(p => p.Responsavel)
                .WithMany(r => r.Contratos)
                .HasForeignKey(c => c.ResponsavelId);

        builder.HasOne(c => c.Vinculo)
                .WithMany(v => v.Contratos)
                .HasForeignKey(c => c.VinculoId);

        builder.HasMany(c => c.Financeiros)
            .WithOne(f => f.Contrato)
            .HasForeignKey(f => f.ContratoId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
