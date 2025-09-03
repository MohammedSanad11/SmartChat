using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Dashboad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Queres.GetCurrentUser;

public class GetCurrentUserQuery:IRequest<CurrentUserDto>
{
    public Guid UserId { get; }
    public GetCurrentUserQuery(Guid userId)
    {
        UserId = userId;
    }
}
