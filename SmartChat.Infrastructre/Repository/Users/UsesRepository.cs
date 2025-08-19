using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Interface.Users;
using SmartChat.Infrastructre.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Repository.Users
{
    public class UsesRepository : BaseSQLRepository<User>,IUsersRepository
    {
        public UsesRepository(SmartChatDbContext smartChatDbContext) : base(smartChatDbContext)
        {
        }
    }
}
