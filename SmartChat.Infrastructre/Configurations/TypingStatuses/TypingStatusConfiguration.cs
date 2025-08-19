using Microsoft.EntityFrameworkCore;
using SmartChat.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartChat.Domain.Entities.TypingStatuses;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartChat.Infrastructre.Configurations.TypingStatuses
{
    public class TypingStatusConfiguration : IEntityTypeConfiguration<TypingStatus>
    {
        public void Configure(EntityTypeBuilder<TypingStatus> builder)
        {
            builder.HasKey(x => new { x.UserId, x.ConversationId });

            builder.HasOne(x => x.Conversion)
                 .WithMany(x => x.typingStatuses).HasForeignKey(x=>x.ConversationId);

            builder.HasOne(x => x.User)
         .WithMany(x => x.typingStatuses).HasForeignKey(x => x.UserId);

            builder.ToTable("TypingStatuses");
        }
    }
}
