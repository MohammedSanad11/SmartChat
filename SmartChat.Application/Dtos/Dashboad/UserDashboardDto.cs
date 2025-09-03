using SmartChat.Application.Dtos.Conversations;
using SmartChat.Application.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Dtos.Dashboad;

public class UserDashboardDto
{
    public Guid UserId { get; set; }
    public int TotalChats { get; set; }
    public int ActiveChats { get; set; }
    public int DailyMessages { get; set; }
    public double ActiveChatPercentage { get; set; }
}