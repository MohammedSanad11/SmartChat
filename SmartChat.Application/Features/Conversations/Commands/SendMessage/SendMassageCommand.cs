using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Commands.SendMessage
{
    public class SendMassageCommand:IRequest<MessageDto>
    {
        public Guid SenderId { get; }
        public Guid ConversationId { get; }
        public string Text { get; }
        public SendMassageCommand(Guid senderId, Guid conversationId, string text)
        {
            SenderId = senderId;
            ConversationId = conversationId;
            Text = text;
        }
    }
}
