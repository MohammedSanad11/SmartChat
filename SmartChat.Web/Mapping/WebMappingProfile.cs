using SmartChat.Application.Dtos.Dashboad;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Web.Views.viewModle;

namespace SmartChat.Web.Mapping
{
    public class WebMappingProfile:Profile
    {
        public WebMappingProfile()
        {

            CreateMap<UserDashboardDto, UserDashboardViewModel>();
            CreateMap<ConversationDto, ConversationVieModel>();



            CreateMap<RegisterViewModel, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<CurrentUserDto, CurrentUserViewModel>();



           
       

            //            CreateMap<ConversationSummaryDto, UserDashboardViewModel>()
            //     .ForMember(dest => dest.ConversationId,
            //         opt => opt.MapFrom(src => src.Id))
            //     .ForMember(dest => dest.ConversationTitle,
            //         opt => opt.MapFrom(src => src.Title ?? "No Title"))
            //     .ForMember(dest => dest.LastMessageAt,
            //         opt => opt.MapFrom(src => src.LastMessageTime))
            //     .ForMember(dest => dest.LastMessageText,
            //         opt => opt.MapFrom(src => src.LastMessageText ?? ""))
            //     .ForMember(dest => dest.LastMessageSenderName,
            //         opt => opt.MapFrom(src => src.LastMessageSenderName ?? ""))
            //     .ForMember(dest => dest.MessageCount,
            //         opt => opt.MapFrom(src => src.MessageCount))
            //    .ForMember(dest => dest.UserId,
            //    opt => opt.MapFrom((src, dest, destMember, context) =>
            //        (Guid?)context.Items["currentUser"]))
            //.ForMember(dest => dest.UserName,
            //    opt => opt.MapFrom((src, dest, destMember, context) =>
            //        (string)context.Items["UserName"]));

            // New Chat Users
            //       CreateMap<UserDto, UserDashboardViewModel>()
            //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName ?? "N/A"))
            //.ForMember(dest => dest.ConversationId, opt => opt.MapFrom(src => (Guid?)null))
            //.ForMember(dest => dest.ConversationTitle, opt => opt.MapFrom(src => "No conversation yet"))
            //.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            //.ForMember(dest => dest.LastMessageAt, opt => opt.MapFrom(src => (DateTime?)null))
            //.ForMember(dest => dest.LastMessageText, opt => opt.MapFrom(src => ""))
            //.ForMember(dest => dest.LastMessageSenderName, opt => opt.MapFrom(src => ""))
            //.ForMember(dest => dest.MessageCount, opt => opt.MapFrom(src => 0));

            // Chat & Messages
            CreateMap<ChatDto, ChatViewModel>();
            
         

            CreateMap<MessageDto, messageViewModel>();
            //CreateMap<ConversationDto, ChatViewModel>()
            //  .ForMember(dest => dest.Title,
            //      opt => opt.MapFrom((src, dest, destMember, context) =>
            //      {
            //          var currentUserId = (Guid)context.Items["CurrentUserId"];

            //          if (src.Users == null || !src.Users.Any())
            //          {
            //              Console.WriteLine("⚠️ src.Users is NULL or EMPTY");
            //              return "Conversation";
            //          }

            //          foreach (var u in src.Users)
            //          {
            //              Console.WriteLine($"User in Conversation: {u.Id} - {u.Name}");
            //          }

            //          var receiver = src.Users.FirstOrDefault(u => u.Id != currentUserId);
            //          Console.WriteLine($"CurrentUserId: {currentUserId}, Receiver: {receiver?.Name}");

            //          return receiver?.Name ?? "Conversation";
            //      }))
            //  .ForMember(dest => dest.CurrentUserId,
            //      opt => opt.MapFrom((src, dest, destMember, context) =>
            //          (Guid)context.Items["CurrentUserId"]))
            //  .ForMember(dest => dest.Messages,
            //      opt => opt.MapFrom(src => src.Messages));
        }
    }
}

