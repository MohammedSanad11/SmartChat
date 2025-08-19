using SmartChat.Domain.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Domain.Interface.Messages
{
    public interface IMessageRepository:IRepository<Message>
    {
    }
}
