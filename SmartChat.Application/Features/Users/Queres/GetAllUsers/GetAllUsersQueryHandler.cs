using AutoMapper;
using AutoMapper.QueryableExtensions.Impl;
using Microsoft.EntityFrameworkCore;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Users;
using SmartChat.Domain.Interface;
using SmartChat.Domain.Interface.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Queres.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUserQuery, List<UserDto>>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request)
        {
            var users = await _uintOfWork._UsersRepository.GetAllAsync(
           include: query => query.Include(u => u.Role),
           AsNoTracking: true);

            return  _mapper.Map<List<UserDto>>(users);
        }
    }
}
