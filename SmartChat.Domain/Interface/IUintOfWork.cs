using Microsoft.VisualBasic;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.Roles;
using SmartChat.Domain.Entities.TypingStatuses;
using SmartChat.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Domain.Interface;

public interface IUintOfWork
{
    public IRepository<Role> _RolesRepository { get; }
    public IRepository<User> _UsersRepository  { get; }
    public IRepository<Conversation> _ConversationsRepository  { get; }
    public IRepository<Message> _MessagesRepository { get; }
    public IRepository<TypingStatus> _TypingStatusRepository { get; }
    public void SaveChange();
     Task SaveChangeAsync();

}
