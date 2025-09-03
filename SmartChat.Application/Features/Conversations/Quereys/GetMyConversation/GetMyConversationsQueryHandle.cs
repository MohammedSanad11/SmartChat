using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Conversations;
using SmartChat.Application.Dtos.Dashboad;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Quereys.GetMyConversation
{
     
    public class GetMyConversationsQueryHandle : IRequestHandler<GetMyConversationsQuery, List<ConversationDto>>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetMyConversationsQueryHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<List<ConversationDto>> Handle(GetMyConversationsQuery request)
        {

            var conversations = await 
                _uintOfWork._ConversationsRepository.FindAsync(
          c => c.UserId == request.UserId || c.AgentId == request.UserId,
          q => q.Include(c => c.messages)
                .Include(c => c.User)
                .Include(c => c.Agent),
          AsNoTracking: true);


            var conversationDto = _mapper.Map<List<ConversationDto>>(conversations);



            return conversationDto;
        }
    }
}
