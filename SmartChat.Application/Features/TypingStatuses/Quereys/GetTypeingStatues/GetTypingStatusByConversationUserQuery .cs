using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.TypingStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.TypingStatuses.Quereys.GetTypeingStatues
{
    public class GetTypingStatusByConversationUserQuery : IRequest<TypingStatuesDto>
    {
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }
    }
}
