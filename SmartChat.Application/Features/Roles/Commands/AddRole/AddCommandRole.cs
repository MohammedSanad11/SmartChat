using SmartChat.Application.Core.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Roles.Commands.AddRole
{
    public class AddCommandRole:IRequest<Guid>
    {
        public  string Name { get; set; }
    }
}
