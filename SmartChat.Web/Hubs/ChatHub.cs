using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Interface;
using SmartChat.Infrastructre.Repository;
using System.Threading.Tasks;

namespace SmartChat.Web.Hubs
{
    [Authorize]
    public class ChatHub:Hub
    {
        private readonly IUintOfWork _uintOfWork;

        public ChatHub(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task JoinConversation(Guid senderId, Guid conversationId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());

        }
        public async Task LeaveConversation(Guid conversationId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId.ToString());
        }
        public async Task SendMessage(Guid senderId, Guid conversationId, string message)
        {
           
            var newMessage = new Message
            {
                Id = Guid.NewGuid(),
                ConversationId = conversationId,
                SenderId =senderId,
                Text = message,
                CreatedAt = DateTime.Now
            };

            await _uintOfWork._MessagesRepository.AddAsync(newMessage);
            await _uintOfWork.SaveChangeAsync();

            await Clients.Group(conversationId.ToString())
                .SendAsync("ReceiveMessage", new
                {
                    UserId = senderId,
                    Text = message,
                    CreatedAt = DateTime.Now
                });
        }
        public async Task SendTypingStatus(Guid conversationId, Guid userId, bool isTyping)
        {
            await Clients.OthersInGroup(conversationId.ToString())
                .SendAsync("ReceiveTypingStatus", new
                {
                    UserId = userId,
                    IsTyping = isTyping
                });
        }

    }
}
