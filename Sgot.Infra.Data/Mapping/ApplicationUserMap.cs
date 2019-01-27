using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Infra.Data.Mapping
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Ignore(u => u.Roles);
            builder.Ignore(u => u.Claims);
        }
    }
}
