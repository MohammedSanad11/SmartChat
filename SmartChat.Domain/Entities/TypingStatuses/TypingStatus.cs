using Microsoft.VisualBasic;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Domain.Entities.TypingStatuses;

public class TypingStatus
{
    public Guid ConversationId { get; set; }
    public Conversation Conversion { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }   
    public bool IsTyping { get; set; } 
    public DateTime CreatedAt { get; set; }
}
