using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Users;
using SmartChat.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Queres.GetNewChat
{
    public class GetNewChatUsersQuery:IRequest<List<UserDto>>
    {
        public Guid CurrentUserId { get; set; }
        public GetNewChatUsersQuery(Guid currentUserId)
        {
            CurrentUserId = currentUserId;
        }


    }
}
