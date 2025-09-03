using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Dashboad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Queres.GetUserDashboard
{
    public class GetUserDashboardQuery:IRequest<UserDashboardDto>
    {
        public Guid UserId { get; set; }
    }
}
