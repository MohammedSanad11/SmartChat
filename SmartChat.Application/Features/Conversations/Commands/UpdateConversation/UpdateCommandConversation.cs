using SmartChat.Application.Core.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Commands.UpdateConversation
{
    public class UpdateCommandConversation:IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AgentId { get; set; }
    }
}
