using AutoMapper;
using SmartChat.Application.Dtos.Conversations;
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
            
            // User Mapping
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PassWord))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UesrName)) 
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.PassWord, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Conversations, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedConversations, opt => opt.Ignore())
                .ForMember(dest => dest.typingStatuses, opt => opt.Ignore());

            // Conversation Mapping
            CreateMap<Conversation, ConversationDto>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreateAt))
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => new List<User> { src.User, src.Agent })) 
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.messages));

            CreateMap<ConversationDto, Conversation>();

            // Message Mapping
            CreateMap<Message, MessageDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
              .ForMember(dest => dest.ConversationId, opt => opt.MapFrom(src => src.ConversationId))
              .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
              .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId));

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