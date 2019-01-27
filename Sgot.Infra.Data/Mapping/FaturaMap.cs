using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Infra.Data.Mapping
{
    public class FaturaMap : IEntityTypeConfiguration<Fatura>
    {
        public void Configure(EntityTypeBuilder<Fatura> builder)
        {
            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.Invalid);
            builder.HasKey(fat => fat.Id).HasName("FaturaIdPk");
            builder.Property(fat => fat.DataPagamento)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(fat => fat.Id).HasColumnName("FaturaId");
            //var converter = new EnumToStringConverter<FormaPagamento>();
            builder.Property(fat => fat.FormaPagamento)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(fat => fat.NumeroParcelas)
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(fat => fat.Sinal)
                .HasColumnType("numeric(10,2)");

            builder.Property(fat => fat.Total).HasColumnName("Total_A_Pagar")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(fat => fat.Valor)
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(fat => fat.Valor)
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.HasOne(fat => fat.Pedido)
                .WithOne(p => p.Fatura)
                .HasForeignKey<Fatura>(f => f.PedidoId)
                .IsRequired();

            builder.HasMany(f => f.Parcelas)
                .WithOne(p => p.Fatura)
                .HasForeignKey(p => p.FaturaId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
