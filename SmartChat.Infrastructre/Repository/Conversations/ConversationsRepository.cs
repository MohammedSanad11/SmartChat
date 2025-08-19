using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Interface.Conversations;
using SmartChat.Infrastructre.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Repository.Conversations
{
    public class ConversationsRepository : BaseSQLRepository<Conversation>,IConversationsRepository
    {
        public ConversationsRepository(SmartChatDbContext smartChatDbContext) : base(smartChatDbContext)
        {
        }
    }
}
