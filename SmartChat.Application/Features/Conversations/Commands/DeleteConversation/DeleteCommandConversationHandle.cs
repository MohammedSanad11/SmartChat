using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Commands.DeleteConversation
{
    public class DeleteCommandConversationHandle : IRequestHandler<DeleteCommandConversation, bool>
    {
        private readonly IUintOfWork _uintOfWork;

        public DeleteCommandConversationHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(DeleteCommandConversation request)
        {
            var Conversation = await _uintOfWork._ConversationsRepository.GetByConditionAsync
                (x => x.Id == request.Id);

            if (Conversation == null) return false;


            _uintOfWork._ConversationsRepository.Delete(Conversation);
            _uintOfWork.SaveChange();

            return true;
        }
    }
}
