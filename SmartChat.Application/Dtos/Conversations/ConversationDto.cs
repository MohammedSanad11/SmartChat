using SmartChat.Application.Dtos.Messages;
using SmartChat.Application.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Dtos.Conversations
{
    public class ConversationDto
    {
        public Guid Id { get; set; }           
       public string Title { get; set; }
        public Guid CurrentUserId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? EndedAt { get; set; }
        public int MessageCount { get; set; }
        public string LastMessageText { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public string LastMessageSenderName { get; set; }
        public List<MessageDto> messages { get; set; }
        public Guid AgentId { get; set; }
        public Guid UserId { get; set; }
    }
}
