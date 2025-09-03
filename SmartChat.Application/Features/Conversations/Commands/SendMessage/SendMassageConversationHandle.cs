using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Messages;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Commands.SendMessage
{
    public class SendMassageCommandHandle : IRequestHandler<SendMassageCommand, MessageDto>
    {
        private readonly IUintOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SendMassageCommandHandle(IUintOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MessageDto> Handle(SendMassageCommand request)
        {
            var newMessage = new Message
            {
                Id = Guid.NewGuid(),
                ConversationId = request.ConversationId,
                SenderId = request.SenderId,
                Text = request.Text,
                CreatedAt = DateTime.Now,
            };

            await _unitOfWork._MessagesRepository.AddAsync(newMessage);
            await _unitOfWork.SaveChangeAsync();

            var conversation = await _unitOfWork._ConversationsRepository
            .GetByConditionAsync(
                c => c.Id == request.ConversationId,
                include: query => query
                    .Include(c => c.User)
                    .Include(c => c.Agent),
                AsNoTracking: true
            );

            var sender = await _unitOfWork._UsersRepository
                .GetByConditionAsync(u => u.Id == request.SenderId,
            include: query => query.Include(u => u.Role));


            string senderName = sender.Name;
            string receiverName = (sender.Role.Name == "Admin" || sender.Role.Name == "Agent")
                ? conversation.User.Name
                : conversation.Agent.Name;

            var dto = new MessageDto
            {
                Id = request.SenderId,
                SenderName = senderName,
                ReceiverName = receiverName,
                Text = request.Text,
                CreatedAt = DateTime.Now
            };

            return dto;
        }
    }
}
