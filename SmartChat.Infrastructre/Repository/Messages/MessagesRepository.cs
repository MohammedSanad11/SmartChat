using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Interface.Messages;
using SmartChat.Infrastructre.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Repository.Messages
{
    public class MessagesRepository : BaseSQLRepository<Message>,IMessageRepository
    {
        public MessagesRepository(SmartChatDbContext smartChatDbContext) : base(smartChatDbContext)
        {
        }
    }
}
