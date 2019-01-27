using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Infra.Data.Mapping
{
    public class ParcelaMap : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.Invalid);
            builder.HasKey(p => p.Id).HasName("ParcelaIdPk");
            builder.Property(p => p.Id).HasColumnName("ParcelaId");
            builder.Property(p => p.DataPagamento)
                .HasColumnType("date")                
                .IsRequired();

            builder.Property(p => p.DataVencimento)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.Numero)
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(p => p.Recebido)
                .HasColumnType("boolean")
                .IsRequired();

            builder.Property(p => p.Valor)
                .HasColumnType("numeric(10,2)")
                .IsRequired();
        }
    }
}
