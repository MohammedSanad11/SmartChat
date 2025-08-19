using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Users;
using SmartChat.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.LoginUser.command.testloginuser
{
    public class LoginUserCommand:IRequest<UserDto>
    {
        public LoginUserCommand(string username, string email, string password, Guid roleId)
        {
            Username = username;
            Email = email;
            Password = password;
            RoleId = roleId;
        }

        public string Username { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
    }
}
