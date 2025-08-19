using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Users;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Queres.GetUsersById
{
    public class GetUsersByIdHandleQuery : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUintOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersByIdHandleQuery(IUintOfWork uintOfWork,IMapper mapper)
        {
            _unitOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request)
        {
            var user = await _unitOfWork._UsersRepository.GetByConditionAsync(
           prediecate: u => u.Id == request.Id,
           include: q => q.Include(u => u.Role),
           AsNoTracking: true);
                              

            if (user == null)
                throw new Exception("requst Id is not found");


            return _mapper.Map<UserDto>(user);
        }
    }
}
