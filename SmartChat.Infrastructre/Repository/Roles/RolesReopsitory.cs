using SmartChat.Domain.Entities.Roles;
using SmartChat.Domain.Interface.Roles;
using SmartChat.Infrastructre.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Repository.Roles
{
    public class RolesRepository : BaseSQLRepository<Role>,IRolesRepository
    {
        public RolesRepository(SmartChatDbContext smartChatDbContext) : base(smartChatDbContext)
        {
        }
    }
}
