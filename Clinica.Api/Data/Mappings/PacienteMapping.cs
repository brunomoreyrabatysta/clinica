using Clinica.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinica.Api.Data.Mappings;

public class PacienteMapping : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Paciente");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(300);
        builder.Property(x => x.CPF)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(14);
        builder.Property(x => x.RG)            
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);
        builder.Property(x => x.DataEmisssaoRG)            
            .HasColumnType("DATE");
        builder.Property(x => x.UFEmissaoRG)            
            .HasColumnType("VARCHAR")
            .HasMaxLength(2);
        builder.Property(x => x.Endereco)            
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
        builder.Property(x => x.Complemento)            
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
        builder.Property(x => x.Numero)            
            .HasColumnType("VARCHAR")
            .HasMaxLength(15);
        builder.Property(x => x.Bairro)            
            .HasColumnType("VARCHAR")
            .HasMaxLength(50);
        builder.Property(x => x.Cidade)            
            .HasColumnType("INT");
        builder.Property(x => x.CEP)
            .HasColumnType("VARCHAR")
            .HasMaxLength(10);
        builder.Property(x => x.Naturalidade)
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);
        builder.Property(x => x.Nacionalidade)
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);
        builder.Property(x => x.Sexo)
            .HasColumnType("CHAR")
            .HasMaxLength(1);
        builder.Property(x => x.DataNascimento)
            .IsRequired()
            .HasColumnType("DATE");
        builder.Property(x => x.NumeroTelefone)
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);
        builder.Property(x => x.Email)
            .HasColumnType("VARCHAR")
            .HasMaxLength(150);
    }
}
