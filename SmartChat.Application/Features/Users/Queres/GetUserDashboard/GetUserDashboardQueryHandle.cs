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

namespace SmartChat.Application.Features.Users.Queres.GetUserDashboard
{
    public class GetUserDashboardQueryHandle : IRequestHandler<GetUserDashboardQuery, UserDashboardDto>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetUserDashboardQueryHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<UserDashboardDto> Handle(GetUserDashboardQuery request)
        {

            var user = await _uintOfWork._UsersRepository.GetByConditionAsync(
         prediecate: u => u.Id == request.UserId,
                 include: q => q.Include(c => c.Conversations).Include(m => m.Messages));

            if (user == null)
                throw new Exception("User not found");
            
            var mapped = _mapper.Map<UserDashboardDto>(user);

            return mapped;
        }
    }
}
