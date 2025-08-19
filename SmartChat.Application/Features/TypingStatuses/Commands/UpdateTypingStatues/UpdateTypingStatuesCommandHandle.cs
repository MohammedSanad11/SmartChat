using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.TypingStatuses.Commands.UpdateTypingStatues
{
    public class UpdateTypingStatuesCommandHandle : IRequestHandler<UpdateTypingStatuesCommand, bool>
    {
        private readonly IUintOfWork _uintOfWork;

        public UpdateTypingStatuesCommandHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(UpdateTypingStatuesCommand request)
        {
            var entity = await _uintOfWork._TypingStatusRepository.GetByConditionAsync(
            ts => ts.ConversationId == request.ConversationId && ts.UserId == request.UserId);

            if (entity == null)
                return false;

            entity.IsTyping = request.isTyping;

             _uintOfWork._TypingStatusRepository.Update(entity);
            await _uintOfWork.SaveChangeAsync();

            return true;
        }
    }
}
