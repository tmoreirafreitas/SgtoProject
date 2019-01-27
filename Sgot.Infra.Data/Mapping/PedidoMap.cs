using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Infra.Data.Mapping
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.Invalid);
            builder.HasKey(p => p.Id).HasName("PedidoIdPk");
            builder.Property(p => p.Id).HasColumnName("PedidoId");
            builder.Property(p => p.DataSolicitacao)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.DataEntrega)
                .HasColumnType("date")
                .IsRequired();

            //var converter = new EnumToStringConverter<FormaPagamento>();
            builder.Property(p => p.FormaPagamento)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(p => p.Medico)
                .HasColumnType("varchar(80)")
                .IsRequired();

            builder.Property(p => p.Obs)
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(p => p.Preco)
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(p => p.Servico)
                .HasColumnType("varchar(1000)")
                .IsRequired();

            builder.HasMany(p => p.Oculos)
                .WithOne(o => o.Pedido)
                .HasForeignKey(o => o.PedidoId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
