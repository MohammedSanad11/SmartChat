using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Quereys.GetByIdConversation
{
    public class GetByIdConversationQuery:IRequest<ConversationDto>
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
        public GetByIdConversationQuery(Guid Id, Guid currentUserId)
        {
            this.Id = Id;
            this.CurrentUserId = currentUserId;
        }
    }
}
