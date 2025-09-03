using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Dtos.Dashboad
{
    public class AllChatDashboardDtoItem
    {
        public Guid ConversationId { get; set; }                 
        public string ConversationTitle { get; set; }            
        public DateTime CreatedAt { get; set; }                 
        public string LastMessageText { get; set; }             
        public string LastMessageSenderName { get; set; }     
        public DateTime? LastMessageAt { get; set; }            
        public int MessageCount { get; set; }
    }
}
