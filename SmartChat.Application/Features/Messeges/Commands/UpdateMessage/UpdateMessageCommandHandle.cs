using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Messeges.Commands.UpdateMessage
{
    public class UpdateMessageCommandHandle:IRequestHandler<UpdateMessageCommand,bool>
    {
        private readonly IUintOfWork _uintOfWork;

        public UpdateMessageCommandHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(UpdateMessageCommand request)
        {
            var message = await _uintOfWork._MessagesRepository.GetByConditionAsync(x => x.Id == request.Id);
            if (message == null) return false;

            message.Text = request.Text;
            message.CreatedAt = DateTime.UtcNow;

            _uintOfWork._MessagesRepository.Update(message);
            
            _uintOfWork.SaveChange();

            return true;
        }
    }
}
