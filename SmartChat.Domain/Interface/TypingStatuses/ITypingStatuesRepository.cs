using SmartChat.Domain.Entities.TypingStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Domain.Interface.TypingStatuses
{
    public interface ITypingStatuesRepository:IRepository<TypingStatus>
    {
    }
}
