using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Configurations.Conversations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.HasOne(x => x.User)
         .WithMany(x => x.Conversations)
         .HasForeignKey(x => x.UserId)
         .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Agent)
                   .WithMany(x => x.AssignedConversations)
                   .HasForeignKey(x => x.AgentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Conversations");

        }
    }
}
