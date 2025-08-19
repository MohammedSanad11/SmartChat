using Microsoft.VisualBasic;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Commands.AddConversation
{
    public class AddCommandConversationHandle : IRequestHandler<AddCommandConversation, Guid>
    {
        private readonly IUintOfWork _uintOfWork;

        public AddCommandConversationHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<Guid> Handle(AddCommandConversation request)
        {
            var Conversation = new Conversation
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                AgentId = request.AgentId,
                CreateAt = DateTime.Now,
                EndedAt = DateTime.MinValue,
            };

            await _uintOfWork._ConversationsRepository.AddAsync(Conversation);
            await _uintOfWork.SaveChangeAsync();

            return Conversation.Id;
        }
    }
}
