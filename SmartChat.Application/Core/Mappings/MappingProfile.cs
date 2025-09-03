using AutoMapper;
using SmartChat.Application.Dtos.Conversations;
using SmartChat.Application.Dtos.Dashboad;
using SmartChat.Application.Dtos.Messages;
using SmartChat.Application.Dtos.Roles;
using SmartChat.Application.Dtos.TypingStatuses;
using SmartChat.Application.Dtos.Users;
using SmartChat.Application.Features.TypingStatuses.Commands.AddTypingStatues;
using SmartChat.Application.Features.TypingStatuses.Commands.UpdateTypingStatues;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.Roles;
using SmartChat.Domain.Entities.TypingStatuses;
using SmartChat.Domain.Entities.Users;
using System.Collections.Generic;




namespace SmartChat.Application.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            var adminRoleId = new Guid("86139214-843D-45F0-A61C-6FEAAF35B108");
            var agentRoleId = new Guid("CA4D0F0C-5216-48D8-AB01-B8AAA0496007");
            var userRoleId = new Guid("06B7E2B4-47DE-4C8C-A156-C7B2696E0B3D");

            // User Mapping
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PassWord))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UesrName))
                  .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src =>
                 src.RoleId == adminRoleId ? "Admin" :
                 src.RoleId == agentRoleId ? "Agent" :
                 src.RoleId == userRoleId ? "User" :
                 "User" // fallback
             )).ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
          
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.PassWord, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Conversations, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedConversations, opt => opt.Ignore())
                .ForMember(dest => dest.typingStatuses, opt => opt.Ignore());

            CreateMap<User, UserDashboardDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(srs => srs.Id))
                .ForMember(dest => dest.TotalChats, opt => opt.MapFrom(srs => srs.Conversations.Count))
                .ForMember(dest => dest.ActiveChats, opt => opt.MapFrom(srs => srs.AssignedConversations.Count))
                .ForMember(dest => dest.DailyMessages,
                       opt => opt.MapFrom(src =>
                           src.Messages.Count(m => m.CreatedAt.Date == DateTime.UtcNow.Date))).
                ForMember(dest => dest.ActiveChatPercentage,
                       opt => opt.MapFrom(src =>
                           src.Conversations.Count == 0
                               ? 0
                               : (double)src.AssignedConversations.Count / src.Conversations.Count * 10 ));


            CreateMap<User, CurrentUserDto>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.ConversationCount, opt => opt.MapFrom(src => src.Conversations.Count))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UesrName));


            // Conversation Mapping
            CreateMap<Conversation, ConversationDto>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src =>
        (src.User != null ? src.User.UesrName : "Unknown") +
        " & " +
        (src.Agent != null ? src.Agent.Name : "Unknown")
    ))
           .ForMember(dest => dest.CreatedAt,
               opt => opt.MapFrom(src => src.CreateAt))
           .ForMember(dest => dest.MessageCount,
               opt => opt.MapFrom(src => src.messages.Count))
           .ForMember(dest => dest.LastMessageText,
               opt => opt.MapFrom(src =>
                   src.messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault() != null
                       ? src.messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault().Text
                       : "No messages yet"))
           .ForMember(dest => dest.LastMessageTime,
               opt => opt.MapFrom(src =>
                   src.messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault() != null
                       ? src.messages.OrderByDescending(m => m.CreatedAt).FirstOrDefault().CreatedAt
                       : (DateTime?)null))
       .ForMember(dest => dest.LastMessageTime, opt =>
        opt.MapFrom(src => src.messages
            .OrderByDescending(m => m.CreatedAt)
            .FirstOrDefault().CreatedAt))
    .ForMember(dest => dest.LastMessageSenderName, opt => opt.Ignore()) 
    .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.AgentId))
    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
      .ForMember(dest => dest.messages, opt => opt.MapFrom(src =>
        src.messages.Select(m => new MessageDto
        {
            Id = m.Id,
            Text = m.Text,
            CreatedAt = m.CreatedAt,
            SenderId = m.SenderId,
            SenderName = m.user != null ? m.user.UesrName : "Unknown"
        }).ToList()));



            CreateMap<ConversationDto, Conversation>();

            // Message Mapping
            CreateMap<Message, MessageDto>()
     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
     .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
     .ForMember(dest => dest.ConversationId, opt => opt.MapFrom(src => src.ConversationId))
     .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
     .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId))
     .ForMember(dest => dest.ReceiverName, opt => opt.MapFrom(src =>
         src.SenderId == src.Conversation.AgentId || src.user.Role.Name == "Admin"
             ? src.Conversation.User.Name          
             : src.Conversation.Agent.Name        
     ));

            // Role Mapping
            CreateMap<Role, RoleDto>();

            CreateMap<RoleDto, Role>();

            // Typing Status Mapping
            CreateMap<AddTypingStatuesCommand, TypingStatus>();
            CreateMap<UpdateTypingStatuesCommand, TypingStatus>();
            CreateMap<TypingStatus, TypingStatuesDto>();
        }
    }
}