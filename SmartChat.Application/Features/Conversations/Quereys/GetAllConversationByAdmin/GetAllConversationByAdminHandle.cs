using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartChat.Application.Core;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Conversations;
using SmartChat.Application.Dtos.Dashboad;
using SmartChat.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SmartChat.Application.Features.Conversations.Quereys.GetAllConversationByAdmin.GetAllConversationByAdmin
{
    public class GetAllConversationByAdminHandle : IRequestHandler<GetAllConversationByAdminQuery, AllChatDashboardDto>
    {
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public GetAllConversationByAdminHandle(IUintOfWork uintOfWork, IMapper mapper)
        {
            _uintOfWork = uintOfWork;
            _mapper = mapper;
        }
         
        public async Task<AllChatDashboardDto> Handle(GetAllConversationByAdminQuery request)
        {

            var allUsers = await _uintOfWork._UsersRepository
         .GetAllAsync(include: q => q.Include(u => u.Role));

            var conversation = await _uintOfWork._ConversationsRepository
                .GetAllAsync(include: q => q.Include(c => c.messages)
                                    .Include(c => c.User)
                                    .Include(c => c.Agent),
                 AsNoTracking: true);

            // Map each Conversation entity to ConversationDto individually
            // instead of mapping the entire list at once. This allows us to
            // apply ordering, filtering, and pagination (Skip/Take) on the
            // DTOs before materializing the final List.
            var allDashboardVm = conversation.Select(c => new AllChatDashboardDtoItem
            {
                ConversationId = c.Id,
                ConversationTitle = string.Join(" / ",
                       new[] { c.User?.UesrName, c.Agent?.UesrName }
                           .Where(n => !string.IsNullOrEmpty(n))),
                CreatedAt = c.CreateAt,
                LastMessageText = c.messages.OrderByDescending(m => m.CreatedAt)
                               .FirstOrDefault()?.Text ?? "Hidden",
                LastMessageSenderName = c.messages.OrderByDescending(m => m.CreatedAt)
                                    .FirstOrDefault()?.user?.UesrName ?? "Unknown",
                LastMessageAt = c.messages.OrderByDescending(m => m.CreatedAt)
                             .FirstOrDefault()?.CreatedAt,
                MessageCount = c.messages.Count
            }).ToList();

            var count = allDashboardVm.Count();
       
              var items = allDashboardVm
                  .OrderByDescending(c => c.LastMessageAt ?? c.CreatedAt)
                  .Skip((request.PageNumber - 1) * request.PageSize)
                  .Take(request.PageSize)
                  .ToList();
            var paginatedConversations = new PaginatedList<AllChatDashboardDtoItem>(items, count, request.PageNumber, request.PageSize);

            var summary = new ChatSummaryDto
            {
                TotalUsers = allUsers.Count(),
                TotalAdmins = allUsers.Count(u => u.Role.Name == "Admin"),
                TotalAgents = allUsers.Count(u => u.Role.Name == "Agent"),
                TotalNormalUsers = allUsers.Count(u => u.Role.Name == "User"),
                TotalOpenChats = conversation.Count(c => c.EndedAt == null)
            };

            var dashboardDto = new AllChatDashboardDto
            {
                Summary = summary,
                Conversations = paginatedConversations
            };


            return dashboardDto;
        }
    }
}


