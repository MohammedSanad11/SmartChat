using SmartChat.Domain.Entities.TypingStatuses;
using SmartChat.Domain.Interface.TypingStatuses;
using SmartChat.Infrastructre.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Repository.TypingStatuses
{
    public class TypingStatusesRepository : BaseSQLRepository<TypingStatus>,ITypingStatuesRepository
    {
        public TypingStatusesRepository(SmartChatDbContext smartChatDbContext) : base(smartChatDbContext)
        {
        }
    }
}
