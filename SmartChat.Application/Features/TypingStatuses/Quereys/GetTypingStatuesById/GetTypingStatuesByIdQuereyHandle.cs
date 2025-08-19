using AutoMapper;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.TypingStatuses;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.TypingStatuses.Quereys.GetTypingStatuesById
{
    public class GetTypingStatuesByIdQuereyHandle : IRequestHandler<GetTypingStatuesByIdQuery, TypingStatuesDto>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;


        public GetTypingStatuesByIdQuereyHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<TypingStatuesDto> Handle(GetTypingStatuesByIdQuery request)
        {
            var entity = await _uintOfWork._TypingStatusRepository.GetByConditionAsync(
            ts => ts.ConversationId == request.ConversationId && ts.UserId == request.UserId);

            if (entity == null)
                return null;

            return _mapper.Map<TypingStatuesDto>(entity);
        }
    }
}
