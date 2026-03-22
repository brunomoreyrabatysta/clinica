using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class VinculoMapping : IEntityTypeConfiguration<Vinculo>
{
    public void Configure(EntityTypeBuilder<Vinculo> builder)
    {
        builder.ToTable("Vinculo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(300);

        builder.HasMany(v => v.Contratos)
            .WithOne(c => c.Vinculo)
            .HasForeignKey(c => c.VinculoId)
            .HasPrincipalKey(v => v.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
