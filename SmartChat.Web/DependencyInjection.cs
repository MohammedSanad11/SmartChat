using SmartChat.Application.Core;
using SmartChat.Application.Dtos.Dashboad;
using SmartChat.Application.Features.Conversations.Commands.SendMessage;
using SmartChat.Application.Features.Conversations.Quereys.GetAllConversationByAdmin;
using SmartChat.Application.Features.Conversations.Quereys.GetAllConversationByAdmin.GetAllConversationByAdmin;
using SmartChat.Application.Features.Conversations.Quereys.GetMyConversation;
using SmartChat.Application.Features.Users.Queres.GetCurrentUser;
using SmartChat.Application.Features.Users.Queres.GetNewChat;
using SmartChat.Application.Features.Users.Queres.GetUserDashboard;

namespace Microsoft.Extensions.DependencyInjection;
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryService(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsesRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IMessageRepository, MessagesRepository>();
            services.AddScoped<IConversationsRepository, ConversationsRepository>();
            services.AddScoped<ITypingStatuesRepository, TypingStatusesRepository>();

            return (services);
        }

        public static IServiceCollection AddRondominterfacesService(this IServiceCollection services)
        {
           services.AddScoped<IUintOfWork, UintOfWork>();
           services.AddScoped<ICustomMediator, CustomMediator>();
           services.AddScoped<IRequestHandler<LoginUserCommand, UserDto>, LoginUserCommandHandel>();
           services.AddSignalR();
           services.AddDistributedMemoryCache();
           services.AddControllersWithViews();
            return (services);
        }
         public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
         {
         
             services.AddAutoMapper(typeof(MappingProfile));
             services.AddAutoMapper(typeof(SmartChat.Web.Mapping.WebMappingProfile).Assembly);
             return (services);
         }
    public static IServiceCollection AddCustomMidMediatorServiceOfUser(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddCommandUser, Guid>, AddCommandHandelUser>();
            services.AddScoped<IRequestHandler<DeleteCommandUser, bool>, DeleteCommandHandleUser>();
            services.AddScoped<IRequestHandler<UpdateCommandUser, bool>, UpdateCommandHandleUser>();
            services.AddScoped<IRequestHandler<GetAllUserQuery, List<UserDto>>, GetAllUsersQueryHandler>();
            services.AddScoped<IRequestHandler<GetUserByIdQuery, UserDto>, GetUsersByIdHandleQuery>();
            services.AddScoped<IRequestHandler<GetNewChatUsersQuery, List<UserDto>>, GetNewChatQueryHandle > ();
            services.AddScoped<IRequestHandler<GetCurrentUserQuery, CurrentUserDto>, GetCurrentUserQueryHandle>();
            services.AddScoped<IRequestHandler<GetUserDashboardQuery, UserDashboardDto>, GetUserDashboardQueryHandle>();
            
            
            return (services);
        }

        public static IServiceCollection AddCustomMidMediatorServiceOfRole(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddCommandRole, Guid>, AddRoleCommandHandle>();
            services.AddScoped<IRequestHandler<UpdateCommandRole, bool>, UpdateCommandHandleRole>();
            services.AddScoped<IRequestHandler<DeleteRoleCommand, bool>, DeleteRoleCommandHandle>();
            services.AddScoped<IRequestHandler<GetAllRoleQuery, List<RoleDto>>, GetAllRoleQueryHandle>();
            services.AddScoped<IRequestHandler<GetRoleByIdQuery, RoleDto>, GetRoleByIdQueryHandle>();

            return (services);
        }

        public static IServiceCollection AddCustomMidMediatorServiceOfMessage(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddMessageCommand, Guid>, AddMessageCommandHandle>();
            services.AddScoped<IRequestHandler<DeleteMessageCommand, bool>, DeleteMessageCommandHandle>();
            services.AddScoped<IRequestHandler<UpdateMessageCommand, bool>, UpdateMessageCommandHandle>();
            services.AddScoped<IRequestHandler<GetMessageByIdQuery, MessageDto>, GetMessageByIdQueryHandle>();

            return (services);
        }

        public static IServiceCollection AddCustomMidMediatorServiceOfConversation(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddCommandConversation, Guid>, AddCommandConversationHandle>();
            services.AddScoped<IRequestHandler<DeleteCommandConversation, bool>, DeleteCommandConversationHandle>();
            services.AddScoped<IRequestHandler<UpdateCommandConversation, bool>, UpdateCommandConversationHandle>();
            services.AddScoped<IRequestHandler<GetAllConversationsQuery, List<ConversationDto>>, GetAllConversationsHandleQuery>();
            services.AddScoped<IRequestHandler<GetByIdConversationQuery, ConversationDto>, GetByIdConversationQueryHandle>();
            services.AddScoped<IRequestHandler<GetAllConversationByAdminQuery, AllChatDashboardDto>, GetAllConversationByAdminHandle>();
            services.AddScoped<IRequestHandler<GetMyConversationsQuery, List<ConversationDto>>, GetMyConversationsQueryHandle>();
            services.AddScoped<IRequestHandler<SendMassageCommand,MessageDto>, SendMassageCommandHandle>();

            return (services);
        }

        public static IServiceCollection AddCustomMidMediatorServiceOfTypingStatues(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddTypingStatuesCommand, bool>, AddTypingStatuesHandle>();
            services.AddScoped<IRequestHandler<DeleteTypingStatuesCommand, bool>, DeleteTypingStatuesCommandHandle>();
            services.AddScoped<IRequestHandler<UpdateTypingStatuesCommand, bool>, UpdateTypingStatuesCommandHandle>();
            services.AddScoped<IRequestHandler<GetAllTypingStatusesQuery, List<TypingStatuesDto>>, GetAllTypingStatusesQueryHandle>();
            services.AddScoped<IRequestHandler<GetTypingStatuesByIdQuery, TypingStatuesDto>, GetTypingStatuesByIdQuereyHandle>();

            return (services);
        }

        public static IServiceCollection AddCustomMidMediatorServiceOfLoginUse(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<LoginUserCommand, UserDto>, LoginUserCommandHandel>();
            return (services);
        }

    }

