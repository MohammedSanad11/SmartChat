using SmartChat.Application.Core.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.TypingStatuses.Commands.UpdateTypingStatues
{
    public class UpdateTypingStatuesCommand:IRequest<bool>
    {
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }
        public bool isTyping {  get; set; }
    }
}
