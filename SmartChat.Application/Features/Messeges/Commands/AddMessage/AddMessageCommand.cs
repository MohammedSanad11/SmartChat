using SmartChat.Application.Core.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Messeges.Commands.AddMessage
{
    public class AddMessageCommand:IRequest<Guid>
    {
        public string Text { get; set; }
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }

    }
}
