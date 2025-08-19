using SmartChat.Application.Dtos.Conversations;
using SmartChat.Application.Dtos.Messages;
using SmartChat.Domain.Entities.Conversations;

namespace SmartChat.Web.Views.viewModle
{
    public class ChatViewModel
    {
        public Guid CurrentUserId { get; set; }
        public ConversationDto Conversation { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
