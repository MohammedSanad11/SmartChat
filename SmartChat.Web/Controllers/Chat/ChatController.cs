using Microsoft.AspNetCore.Mvc;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Features.Conversations.Quereys.GetAllConversation;
using SmartChat.Application.Features.Messeges.Commands.AddMessage;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Web.Views.viewModle;
using Microsoft.AspNetCore.SignalR;
using SmartChat.Web.Hubs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using SmartChat.Domain.Entities.Users;
using SmartChat.Application.Dtos.Conversations;
using SmartChat.Application.Dtos.Messages;
using AutoMapper;
using SmartChat.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using SmartChat.Application.Dtos.Dashboad;


namespace SmartChat.Web.Controllers.Chat
{

    public class ChatController : Controller
    {
        private readonly ICustomMediator _mediator;
        private readonly IMapper _mapper;
        public ChatController(ICustomMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid conversationId)
        {
            var currentUserId = Guid.Parse(User.FindFirst("UserId").Value); 

            var conversation = await _mediator.Send(new GetByIdConversationQuery(conversationId, currentUserId));

            if (conversation == null)
                return NotFound();

            return PartialView("ChatBox", conversation);

        }    


    }
}




