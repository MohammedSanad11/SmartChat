using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Dtos.Messages
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid ConversationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid SenderId { get; set; }
    }
}
