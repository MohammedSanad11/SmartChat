using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.TypingStatuses.Commands.DeleteTypingStatues
{
    public class DeleteTypingStatuesCommandHandle : IRequestHandler<DeleteTypingStatuesCommand, bool>
    {
        private readonly IUintOfWork _uintOfWork;

        public DeleteTypingStatuesCommandHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(DeleteTypingStatuesCommand request)
        {
            var entity = await _uintOfWork._TypingStatusRepository.GetByConditionAsync
                (ts => ts.ConversationId == request.ConversationId && ts.UserId == request.UserId);

            if (entity == null) 
            return false;


            _uintOfWork._TypingStatusRepository.Delete(entity);

            _uintOfWork.SaveChange();

            return true;

        }
    }
}
