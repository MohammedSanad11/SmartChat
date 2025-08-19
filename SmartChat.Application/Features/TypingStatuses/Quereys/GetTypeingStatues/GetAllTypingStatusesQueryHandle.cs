using AutoMapper;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.TypingStatuses;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.TypingStatuses.Quereys.GetTypeingStatues
{
    public class GetAllTypingStatusesQueryHandle:IRequestHandler<GetAllTypingStatusesQuery,List<TypingStatuesDto>>
    {
        private readonly IUintOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTypingStatusesQueryHandle(IUintOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TypingStatuesDto>> Handle(GetAllTypingStatusesQuery request)
        {
            var entities = await _unitOfWork._TypingStatusRepository.GetAllAsync();

            return _mapper.Map<List<TypingStatuesDto>>(entities);
        }
    }
}
