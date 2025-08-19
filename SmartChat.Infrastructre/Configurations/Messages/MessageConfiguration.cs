using Microsoft.EntityFrameworkCore;
using SmartChat.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using SmartChat.Domain.Entities.Messages;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartChat.Infrastructre.Configurations.Messages
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id); 
            
            builder.Property(x=>x.Id).ValueGeneratedNever();

            builder.Property(x=>x.Text).
                HasColumnType("nvarchar").
                HasMaxLength(120);

            builder.HasOne(x=>x.Conversation)
                .WithMany(x=>x.messages)
                .HasForeignKey(x=>x.ConversationId);

            builder.ToTable("Messages");
        }
    }
}
