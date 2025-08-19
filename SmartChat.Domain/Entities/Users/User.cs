using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.TypingStatuses;

namespace SmartChat.Domain.Entities.Users;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string UesrName {  get; set; }
    public string PassWord { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public ICollection<Conversation> Conversations { get; set; }
    public ICollection<Conversation> AssignedConversations { get; set; }
    public ICollection<TypingStatus> typingStatuses { get; set; }
    public DateTime CreatedAt { get; set; }
}
