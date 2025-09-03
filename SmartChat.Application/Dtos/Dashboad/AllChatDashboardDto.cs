using SmartChat.Application.Core;
using SmartChat.Application.Dtos.Conversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Dtos.Dashboad
{
    public class AllChatDashboardDto
    {
        public ChatSummaryDto Summary { get; set; }
        public PaginatedList<AllChatDashboardDtoItem> Conversations { get; set; }
    }
}
