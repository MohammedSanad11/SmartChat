using AutoMapper;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Messages;
using SmartChat.Application.Dtos.Roles;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Roles.Queres.GetRoleById
{
    public class GetRoleByIdQueryHandle : IRequestHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<RoleDto> Handle(GetRoleByIdQuery request)
        {
            var roles = await _uintOfWork._RolesRepository.GetByConditionAsync
               (prediecate: m => m.Id == request.Id,
              include: null,
              AsNoTracking: true);

            if (roles == null) return null;

            return _mapper.Map<RoleDto>(roles);
        }
    }
}
