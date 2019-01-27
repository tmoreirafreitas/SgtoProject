using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Infra.Data.Mapping
{
    public class LenteMap : IEntityTypeConfiguration<Lente>
    {
        public void Configure(EntityTypeBuilder<Lente> builder)
        {
            builder.Ignore(e => e.Valid);
            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.Invalid);
            builder.HasKey(l => l.Id).HasName("LenteIdPk");
            builder.Property(l => l.Id).HasColumnName("LenteId");
            builder.Property(l => l.Cyl)
                .HasColumnType("float");

            builder.Property(l => l.Eixo)
                .HasColumnType("smallint");                

            builder.Property(l => l.Grau)
                .HasColumnType("float")
                .IsRequired();

            //var converter = new EnumToStringConverter<LenteType>();
            builder.Property(l => l.LenteType)
                .HasColumnType("varchar(2)")
                .HasMaxLength(2)
                .IsRequired();
        }
    }
}
