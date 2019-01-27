using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Infra.Data.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.Invalid);
            builder.HasKey(c => c.Id).HasName("ClienteIdPk");
            builder.Property(c => c.Id).HasColumnName("ClienteId");
            builder.Property(c => c.Cpf)
                .HasColumnType("varchar(15)")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(c => c.Filiacao)
                .HasColumnType("varchar(150)")
                .HasMaxLength(150);

            builder.Property(c => c.IsSPC);

            builder.Property(c => c.Nascimento)
                .HasColumnType("date");

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(60)")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.Rg)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10);

            //var converter = new EnumToStringConverter<SexoType>();
            builder.Property(c => c.Sexo)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10);

            builder.Property(c => c.Telefone)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();

            builder.HasMany(c => c.Pedidos)
                .WithOne(p => p.Cliente)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasMany(c => c.Faturas)
                .WithOne(f => f.Cliente)
                .HasForeignKey(f => f.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            //Endereco                        
            builder.Property(e => e.Bairro)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(e => e.Cep)
                .HasColumnType("varchar(9)")
                .HasMaxLength(9)
                .IsRequired();

            builder.Property(e => e.Cidade)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(e => e.Complemento)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Estado)
                .HasColumnType("varchar(30)")
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(e => e.Logradouro)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(e => e.Numero)
                .HasColumnType("integer");            
        }
    }
}
