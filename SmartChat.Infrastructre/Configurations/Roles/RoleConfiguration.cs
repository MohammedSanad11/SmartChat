using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartChat.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Configurations.Roles
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name)
                  .HasColumnType("nvarchar")
                  .HasMaxLength(100);

            builder.ToTable("Roles");
        }
    }
}
