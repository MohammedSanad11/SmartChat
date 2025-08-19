using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Conversations;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Conversations.Quereys.GetAllConversation
{
    public class GetAllConversationsHandleQuery : IRequestHandler<GetAllConversationsQuery, List<ConversationDto>>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;
        public GetAllConversationsHandleQuery(IUintOfWork uintOfWork,IMapper mapper)
        {
            this._mapper = mapper;
            _uintOfWork = uintOfWork;
        }

        public async Task<List<ConversationDto>> Handle(GetAllConversationsQuery request)
        {
            var conversation = await _uintOfWork._ConversationsRepository.GetAllAsync(include: query => query
                .Include(c => c.User)
                .Include(c => c.messages),
            AsNoTracking: true);

            return _mapper.Map<List<ConversationDto>>(conversation);
        }
    }
}
