using AutoMapper;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Messages;
using SmartChat.Application.Dtos.Roles;
using SmartChat.Domain.Entities.Roles;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Roles.Queres
{
    public class GetAllRoleQueryHandle : IRequestHandler<GetAllRoleQuery, List<RoleDto>>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;
        public GetAllRoleQueryHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<List<RoleDto>> Handle(GetAllRoleQuery request)
        {
            var roles = await _uintOfWork._RolesRepository.GetAllAsync
               (
              include: null,
              AsNoTracking: true);


            if (roles == null) return null;

            return _mapper.Map<List<RoleDto>>(roles);
        }
    }
}
