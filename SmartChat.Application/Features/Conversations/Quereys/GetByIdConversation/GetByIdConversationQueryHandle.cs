using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Conversations;
using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Quereys.GetByIdConversation
{
    public class GetByIdConversationQueryHandle : IRequestHandler<GetByIdConversationQuery, ConversationDto>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetByIdConversationQueryHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<ConversationDto> Handle(GetByIdConversationQuery request)
        {
           
            var conversation = await _uintOfWork._ConversationsRepository
             .GetByConditionAsync(
           c => c.Id == request.Id,
        include: query => query
            .Include(c => c.User)
            .Include(c => c.Agent)
            .Include(c => c.messages)
                .ThenInclude(m => m.user),
        AsNoTracking: true);


            if (conversation == null)
                return null;

            var result =_mapper.Map<ConversationDto>(conversation);
            result.CurrentUserId = request.CurrentUserId;
            return result;
        }
    }
}
