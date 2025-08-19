using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Messeges.Commands.DeleteMassege
{
    public class DeleteMessageCommandHandle : IRequestHandler<DeleteMessageCommand, bool>
    {
        public readonly IUintOfWork _uintOfWork;

        public DeleteMessageCommandHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(DeleteMessageCommand request)
        {
            var Message = await _uintOfWork._MessagesRepository.GetByConditionAsync(x => x.Id == request.Id);

            if (Message == null)
                return false;

            _uintOfWork._MessagesRepository.Delete(Message);  

            _uintOfWork.SaveChange();

            return true;
        }
    }
}
