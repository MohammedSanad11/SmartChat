using AutoMapper;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.TypingStatuses;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.TypingStatuses.Quereys.GetTypeingStatues
{
    public class GetTypingStatusByConversationUserQueryHandler : IRequestHandler<GetTypingStatusByConversationUserQuery, TypingStatuesDto>
    {
        private readonly IUintOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public async Task<TypingStatuesDto> Handle(GetTypingStatusByConversationUserQuery request)
        {

            var status = await _unitOfWork._TypingStatusRepository.GetByConditionAsync(
            ts => ts.ConversationId == request.ConversationId && ts.UserId == request.UserId,
            AsNoTracking: true);

            return _mapper.Map<TypingStatuesDto>(status);

        }
    }
}
