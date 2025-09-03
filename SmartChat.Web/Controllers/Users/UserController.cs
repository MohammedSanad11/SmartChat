using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Dtos.Dashboad;
using SmartChat.Application.Dtos.Users;
using SmartChat.Application.Features.Conversations.Quereys.GetAllConversationByAdmin;
using SmartChat.Application.Features.Conversations.Quereys.GetMyConversation;
using SmartChat.Application.Features.Users.Commands.AddUsers;
using SmartChat.Application.Features.Users.Commands.DeleteUsers;
using SmartChat.Application.Features.Users.Commands.UpdateUsers;
using SmartChat.Application.Features.Users.Queres.GetAllUsers;
using SmartChat.Application.Features.Users.Queres.GetCurrentUser;
using SmartChat.Application.Features.Users.Queres.GetNewChat;
using SmartChat.Application.Features.Users.Queres.GetUserDashboard;
using SmartChat.Application.Features.Users.Queres.GetUsersById;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Messages;
using SmartChat.Domain.Entities.Users;
using SmartChat.Web.Mapping;
using SmartChat.Web.Views.viewModle;
using System.Linq;

namespace SmartChat.Web.Controllers.Users
{
    public class UserController : Controller
    {
        private readonly ICustomMediator _mediator;
        private readonly IMapper _mapper;
        public UserController(ICustomMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            var dto = await _mediator.Send(new GetUserDashboardQuery { UserId = userId });

            var view = _mapper.Map<UserDashboardViewModel>(dto);

            return View(view);

        }

        public async Task<IActionResult> Profile()
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            var currentUser =await _mediator.Send(new GetCurrentUserQuery( userId ));

            var view = _mapper.Map<CurrentUserViewModel>(currentUser);

            return PartialView("Profile", view);
        }

        public async Task<IActionResult> MyChats()
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);


            var myConversations = await _mediator.Send(new GetMyConversationsQuery(userId));

            var view = _mapper.Map<List<ConversationVieModel>>(myConversations);

            return PartialView("MyChats", view);
        }

        public async Task<IActionResult> AllConversations()
        {
            var allConversationsDashboard = await _mediator.Send(new GetAllConversationByAdminQuery());

            return PartialView("AllConversations", allConversationsDashboard);
        }

        public async Task<IActionResult> NewChat()
        {
            var currentUserId = Guid.Parse(User.FindFirst("UserId").Value);
            var users = await _mediator.Send(new GetNewChatUsersQuery(currentUserId));
            return PartialView("NewChat", users);
        }

        [HttpPost]
        public async Task<IActionResult> StartConversation(Guid agentId)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value);

            var currentUser = await _mediator.Send(new GetCurrentUserQuery(userId));
            var currentUserId = currentUser.Id;

            var conversationId = await _mediator.Send(new AddCommandConversation
            {
                UserId = currentUserId,
                AgentId = agentId
            });

            return Json(new { id = conversationId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConversation(Guid id)
        {
            var result = await _mediator.Send(new DeleteCommandConversation { Id = id });

            if (result)
            {
                return Json(new { success = true, message = "Conversation deleted successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Conversation not found!" });
            }
        }

    }
}
