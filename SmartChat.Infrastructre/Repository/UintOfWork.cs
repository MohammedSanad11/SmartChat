using Microsoft.VisualBasic;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.Roles;
using SmartChat.Domain.Entities.TypingStatuses;
using SmartChat.Domain.Entities.Users;
using SmartChat.Domain.Interface;
using SmartChat.Infrastructre.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Infrastructre.Repository
{
    public class UintOfWork : IUintOfWork
    {
        private readonly SmartChatDbContext _smartChatDbContext;

        public IRepository<Role> _RolesRepository { get; }
        public IRepository<User> _UsersRepository { get; }
        public IRepository<Conversation> _ConversationsRepository { get; }
        public IRepository<Message> _MessagesRepository { get; }
        public IRepository<TypingStatus> _TypingStatusRepository { get; }

        public UintOfWork(SmartChatDbContext smartChatDbContext)
        {
            _smartChatDbContext = smartChatDbContext;


            _RolesRepository = new BaseSQLRepository<Role>(_smartChatDbContext); 
            _MessagesRepository = new BaseSQLRepository<Message>(_smartChatDbContext);
            _UsersRepository = new BaseSQLRepository<User>(_smartChatDbContext);
            _ConversationsRepository = new BaseSQLRepository<Conversation>(_smartChatDbContext);
            _TypingStatusRepository = new BaseSQLRepository<TypingStatus>(_smartChatDbContext);
        }

        public void SaveChange()
        {
           _smartChatDbContext.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await _smartChatDbContext.SaveChangesAsync();
        }
    }
}
