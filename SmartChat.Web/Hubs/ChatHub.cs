using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using SmartChat.Application.Features.Conversations.Commands.SendMessage;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Interface;
using SmartChat.Infrastructre.Repository;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartChat.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly ICustomMediator _mediator;


        public ChatHub(ICustomMediator mediator, IUintOfWork uintOfWork)
        {
            _mediator = mediator;
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
            try
            {
                var messageDto = await _mediator.Send(new SendMassageCommand(senderId, conversationId, message));

                await Clients.Group(conversationId.ToString())
                    .SendAsync("ReceiveMessage", messageDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendMessage error: " + ex.ToString());
                throw;
            }
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