using SmartChat.Domain.Entities.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Domain.Interface.Conversations
{
    public interface IConversationsRepository:IRepository<Conversation>
    {
    }
}
