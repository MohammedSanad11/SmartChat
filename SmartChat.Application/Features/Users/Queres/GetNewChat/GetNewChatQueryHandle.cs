using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Users;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChat.Application.Features.Users.Queres.GetNewChat
{
    
    public class GetNewChatQueryHandle : IRequestHandler<GetNewChatUsersQuery, List<UserDto>>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetNewChatQueryHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetNewChatUsersQuery request)
        {
            var allUsers = await _uintOfWork._UsersRepository
                .FindAsync(u => u.Id != request.CurrentUserId,
                            include: q => q.Include(u => u.Role),
                            AsNoTracking: true);

            var currentUserConversations = await _uintOfWork._ConversationsRepository
                .FindAsync(c => c.UserId == request.CurrentUserId || c.AgentId == request.CurrentUserId,
                            include: q => q.Include(c => c.User).Include(c => c.Agent),
                            AsNoTracking: true);

            var existingChatUserIds = currentUserConversations
                .Select(c => c.UserId == request.CurrentUserId ? c.AgentId : c.UserId)
                .ToHashSet();

            var availableUsers = allUsers.Where(u => !existingChatUserIds.Contains(u.Id)).ToList();

            return _mapper.Map<List<UserDto>>(availableUsers);
        }
    }
}
