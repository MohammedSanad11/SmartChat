using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.Roles;
using SmartChat.Domain.Entities.TypingStatuses;
using SmartChat.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace SmartChat.Infrastructre.SQL;

public class SmartChatDbContext: DbContext
{
    DbSet<Role> Roles {  get; set; }
    DbSet<User> Users {  get; set; }
    DbSet<Conversation> conversations { get; set; }
    DbSet<Message> messages { get; set; }
    DbSet<TypingStatus> typingStatuses { get; set; }

    public SmartChatDbContext(DbContextOptions<SmartChatDbContext> dbContextOptions):base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(SmartChatDbContext).Assembly);
    }
}
