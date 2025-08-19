using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Queres.GetUsersById
{
    public class GetUserByIdQuery:IRequest<UserDto>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid Id)
        {
            this.Id = Id;            
        }
    }
}
