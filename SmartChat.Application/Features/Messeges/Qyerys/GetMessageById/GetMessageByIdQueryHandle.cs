using AutoMapper;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Messages;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Messeges.Qyerys.GetMessageById
{
    public class GetMessageByIdQueryHandle : IRequestHandler<GetMessageByIdQuery, MessageDto>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetMessageByIdQueryHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<MessageDto> Handle(GetMessageByIdQuery request)
        {
            var message = await _uintOfWork._MessagesRepository.GetByConditionAsync
                (prediecate: m => m.Id == request.Id,
               include: null,
               AsNoTracking: true);

            if (message == null) return null;

            return _mapper.Map<MessageDto>(message);
        }
    }
}
