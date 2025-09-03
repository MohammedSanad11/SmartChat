using SmartChat.Application.Dtos.Conversations;
using SmartChat.Application.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Dtos.Dashboad;

public class ChatDto
{
    public Guid CurrentUserId { get; set; }
    public ConversationDto Conversation { get; set; }
    public List<MessageDto> Messages { get; set; }
}
