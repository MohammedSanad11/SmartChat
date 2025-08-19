using SmartChat.Application.Core.Interfasces;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateCommandHandleRole : IRequestHandler<UpdateCommandRole, bool>
    {
        private readonly IUintOfWork _uintOfWork;

        public UpdateCommandHandleRole(IUintOfWork uintOfWork)
        {
            this._uintOfWork = uintOfWork;
        }

        public async Task<bool> Handle(UpdateCommandRole request)
        {
            var role = await _uintOfWork._RolesRepository.GetByConditionAsync(x=>x.Id == request.Id);
            if (role == null)
                throw new Exception("Role not found");

            role.Name = request.Name;

            await _uintOfWork.SaveChangeAsync();

            return true;
        }
    }
}
