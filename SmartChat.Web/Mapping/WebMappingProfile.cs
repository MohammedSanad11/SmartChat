using SmartChat.Web.Views.viewModle;

namespace SmartChat.Web.Mapping
{
    public class WebMappingProfile:Profile
    {
        public WebMappingProfile()
        {
            CreateMap<RegisterViewModel, UserDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.RoleName, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
             
                      CreateMap<ConversationDto, UserDashboardViewModel>()
                     .ForMember(dest => dest.ConversationId, opt => opt.MapFrom(src => src.Id))
                     .ForMember(dest => dest.ConversationTitle, opt => opt.MapFrom(src =>
                      src.Users != null && src.Users.Any()
                      ? string.Join(", ", src.Users.Select(u => u.Name))
                     : "No Users"))
                     .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                     .ForMember(dest => dest.LastMessageAt, opt => opt.MapFrom(src =>
                     src.Messages != null && src.Messages.Any()
                    ? src.Messages.Max(m => m.CreatedAt)
                    : (DateTime?)null))
                   .ForMember(dest => dest.LastMessageText, opt => opt.MapFrom(src =>
                     src.Messages != null && src.Messages.Any()
                    ? src.Messages.OrderByDescending(m => m.CreatedAt).First().Text
                     : string.Empty));
        }
    }
}
