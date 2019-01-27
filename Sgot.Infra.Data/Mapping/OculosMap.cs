using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Infra.Data.Mapping
{
    public class OculosMap : IEntityTypeConfiguration<Oculos>
    {
        public void Configure(EntityTypeBuilder<Oculos> builder)
        {
            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.Invalid);
            builder.HasKey(o => o.Id).HasName("OculosIdPk");
            builder.Property(o => o.Id).HasColumnName("OculosId");
            builder.Property(o=>o.Adicao)
                .HasColumnType("float");

            builder.Property(o => o.ALT)
                .HasColumnType("float");

            builder.Property(o => o.Cor)
                .HasColumnType("varchar(30)");

            builder.Property(o => o.DP)
                .HasColumnType("float");            

            builder.HasMany(o => o.Lentes)
                .WithOne(l => l.Oculos)
                .HasForeignKey(l => l.OculosId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
