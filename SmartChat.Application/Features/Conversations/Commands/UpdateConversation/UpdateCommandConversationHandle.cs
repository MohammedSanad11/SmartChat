using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Commands.UpdateConversation
{
    public class UpdateCommandConversationHandle : IRequestHandler<UpdateCommandConversation, bool>
    {
        private readonly IUintOfWork _uintOfWork;

        public UpdateCommandConversationHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(UpdateCommandConversation request)
        {
            var Conversation = await
                _uintOfWork._ConversationsRepository.GetByConditionAsync(c => c.Id == request.Id);
        
          if (Conversation == null) 
                return false;
        
             Conversation.UserId = request.UserId;
             Conversation.AgentId = request.AgentId;
             Conversation.CreateAt = DateTime.UtcNow;
             Conversation.EndedAt = DateTime.MinValue;

            _uintOfWork._ConversationsRepository.Update(Conversation);
        
            _uintOfWork.SaveChange();

            return true;
        }
    }
}
