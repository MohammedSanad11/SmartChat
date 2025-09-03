using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Dashboad;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Queres.GetCurrentUser
{
    public class GetCurrentUserQueryHandle : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetCurrentUserQueryHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<CurrentUserDto> Handle(GetCurrentUserQuery request)
        {
            var user = await _uintOfWork._UsersRepository.GetByConditionAsync(
                 u => u.Id == request.UserId,
                 include: q => q.Include(u => u.Conversations)
                   .Include(u => u.Role), 
                  AsNoTracking: true
                 ); 

            var mapped = _mapper.Map<CurrentUserDto>(user);

            if (user == null)
                return null;

            return mapped;
        }
    }
}
