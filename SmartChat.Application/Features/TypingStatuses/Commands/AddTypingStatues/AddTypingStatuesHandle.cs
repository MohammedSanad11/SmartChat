using AutoMapper;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Entities.TypingStatuses;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.TypingStatuses.Commands.AddTypingStatues
{
    public class AddTypingStatuesHandle : IRequestHandler<AddTypingStatuesCommand, bool>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public AddTypingStatuesHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(AddTypingStatuesCommand request)
        {
            var typingStatus = _mapper.Map<TypingStatus>(request);
           

            await _uintOfWork._TypingStatusRepository.AddAsync(typingStatus);

            await _uintOfWork.SaveChangeAsync();    

            return true;
        }
    }
}
