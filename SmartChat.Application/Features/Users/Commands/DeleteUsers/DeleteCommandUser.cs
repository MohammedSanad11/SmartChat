using SmartChat.Application.Core.Interfasces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Commands.DeleteUsers
{
    public class DeleteCommandUser:IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
