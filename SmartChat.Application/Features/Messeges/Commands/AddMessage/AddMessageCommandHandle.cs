using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Messeges.Commands.AddMessage
{
    public class AddMessageCommandHandle : IRequestHandler<AddMessageCommand, Guid>
    {
        private readonly IUintOfWork _uintOfWork;

        public AddMessageCommandHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<Guid> Handle(AddMessageCommand request)
        {
            var Message = new Message
            {
                Id = Guid.NewGuid(),
                Text = request.Text,
                SenderId = request.SenderId,
                ConversationId = request.ConversationId,
            };

            await _uintOfWork._MessagesRepository.AddAsync(Message);
            await _uintOfWork.SaveChangeAsync();
            
            return Message.Id;
        }
    }
}
