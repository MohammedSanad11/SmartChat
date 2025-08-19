using SmartChat.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Domain.Interface.Users
{
    public interface IUsersRepository:IRepository<User>
    {
    }
}
