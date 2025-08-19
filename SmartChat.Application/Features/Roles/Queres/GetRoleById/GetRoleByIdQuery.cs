using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Roles.Queres.GetRoleById
{
    public class GetRoleByIdQuery:IRequest<RoleDto>
    {
        public Guid Id { get; set; }
    }
}
