using SmartChat.Application.Core.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Commands.AddConversation
{
    public class AddCommandConversation:IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid AgentId { get; set; }

    }
}
