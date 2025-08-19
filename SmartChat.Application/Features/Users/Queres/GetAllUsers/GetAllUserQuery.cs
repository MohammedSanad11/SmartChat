using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Queres.GetAllUsers
{
    public class GetAllUserQuery:IRequest<List<UserDto>>
    {
    }
}
