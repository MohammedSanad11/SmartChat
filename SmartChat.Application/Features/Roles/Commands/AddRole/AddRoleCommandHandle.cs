using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Entities.Roles;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Roles.Commands.AddRole
{
    public class AddRoleCommandHandle : IRequestHandler<AddCommandRole, Guid>
    {
        private readonly IUintOfWork _uintOfWork;

        public AddRoleCommandHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<Guid> Handle(AddCommandRole request)
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
            };

            await _uintOfWork._RolesRepository.AddAsync(role);
            await _uintOfWork.SaveChangeAsync();

            return role.Id;
        }
    }
}
