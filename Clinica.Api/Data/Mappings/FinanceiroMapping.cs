using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class FinanceiroMapping : IEntityTypeConfiguration<Financeiro>
{
    public void Configure(EntityTypeBuilder<Financeiro> builder)
    {
        builder.ToTable("Financeiro");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.ContratoId)
            .IsRequired()
            .HasColumnType("INT");
        builder.Property(x => x.DataEmissao)
            .IsRequired()
            .HasColumnType("DATE");
        builder.Property(x => x.DataVencimento)
            .IsRequired()
            .HasColumnType("DATE");
        builder.Property(x => x.DataPagamento)
            .HasColumnType("DATE");
        builder.Property(x => x.DataCancelamento)
            .HasColumnType("DATE");
        builder.Property(x => x.Valor)
            .IsRequired()
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorMora)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorJuros)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorDesconto)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.ValorPago)
            .HasColumnType("NUMERIC(15,2)");
        builder.Property(x => x.Situacao)
            .IsRequired()
            .HasColumnType("CHAR")
            .HasMaxLength(1);
        builder.Property(x => x.NumeroParcela)            
            .HasColumnType("INT");
        builder.Property(x => x.TipoFinanceiro)
            .IsRequired()
            .HasColumnType("CHAR")
            .HasMaxLength(1);
        builder.Property(x => x.Observacao)            
            .HasColumnType("VARCHAR")
            .HasMaxLength(8000);


        builder.HasOne(f => f.Contrato)
                .WithMany(c => c.Financeiros)
                .HasForeignKey(f => f.ContratoId);
    }
}
