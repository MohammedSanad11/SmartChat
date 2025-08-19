using AutoMapper;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Users;
using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.LoginUser.command.testloginuser
{
    public class LoginUserCommandHandel:IRequestHandler<LoginUserCommand,UserDto>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;
        public LoginUserCommandHandel(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(LoginUserCommand request)
        {
            var user = await _uintOfWork._UsersRepository.GetByConditionAsync(u =>
              u.Name == request.Username &&
              u.PassWord == request.Password &&
              u.RoleId == request.RoleId);

            return _mapper.Map<UserDto>(user);

        }
    }
}
