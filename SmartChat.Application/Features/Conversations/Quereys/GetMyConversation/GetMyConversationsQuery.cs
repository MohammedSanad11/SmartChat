using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Conversations;
using SmartChat.Application.Dtos.Dashboad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Quereys.GetMyConversation
{
    public class GetMyConversationsQuery:IRequest<List<ConversationDto>>
    {
        public Guid UserId  { get; set; }

        public GetMyConversationsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
