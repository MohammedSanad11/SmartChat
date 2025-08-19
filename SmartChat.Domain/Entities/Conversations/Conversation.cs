using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.TypingStatuses;

namespace SmartChat.Domain.Entities.Conversations;
public class Conversation
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid AgentId { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public User User { get; set; }
    public User Agent { get; set; }
    public ICollection<TypingStatus> typingStatuses { get; set; }
    public ICollection<Message> messages { get; set; }

}
