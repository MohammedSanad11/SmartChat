using SmartChat.Domain.Entities.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Dtos.TypingStatuses
{
    public class TypingStatuesDto
    {
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }
        public bool IsTyping { get; set; }
    }
}
