using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartChat.Domain.Entities.Roles;
using SmartChat.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Configurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);
            
            builder.Property(x => x.Email)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            builder.Property(x => x.PassWord)
              .HasColumnType("nvarchar")
              .HasMaxLength(100);

            builder.Property(x => x.UesrName)
              .HasColumnType("nvarchar")
              .HasMaxLength(100);

            builder.HasOne(x => x.Role).WithMany(x => x.Users)
                .HasForeignKey(x=>x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Users");
        }
    }
}
