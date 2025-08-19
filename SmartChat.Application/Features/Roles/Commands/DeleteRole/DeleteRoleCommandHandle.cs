using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandle : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IUintOfWork _uintOfWork;

        public DeleteRoleCommandHandle(IUintOfWork uintOfWork)
        {
            _uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(DeleteRoleCommand request)
        {
            var roles = await _uintOfWork._RolesRepository.GetByConditionAsync(x => x.Id == request.Id);

            _uintOfWork._RolesRepository.Delete(roles);

            _uintOfWork.SaveChange();
            return true;   
        }
    }
}
