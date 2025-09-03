using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Roles;
using SmartChat.Domain.Entities.Users;

namespace SmartChat.Domain.Entities.Messages
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ConversationId {  get; set; } 
        public User user { get; set; }  
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRole { get; set; }
        public bool IsAgent { get; set; }
        public Conversation Conversation { get; set; }
    }
}
