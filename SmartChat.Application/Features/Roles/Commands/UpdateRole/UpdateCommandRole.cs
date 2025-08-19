using SmartChat.Application.Core.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateCommandRole:IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
