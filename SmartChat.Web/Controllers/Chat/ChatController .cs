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


namespace SmartChat.Web.Controllers.Chat
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly ICustomMediator _mediator;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IUintOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChatController(ICustomMediator mediator, IHubContext<ChatHub> chatHubContext, IUintOfWork unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _chatHubContext = chatHubContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    
     
       [HttpGet]
       public async Task<IActionResult> Index(Guid conversationId)
       {
           var username = User.Identity?.Name;
           if (string.IsNullOrEmpty(username))
               return RedirectToAction("Login", "Auth");

           var currentUser = await _unitOfWork._UsersRepository
               .GetByConditionAsync(u => u.UesrName == username);

            if (currentUser == null) return Unauthorized();
 
       var conversations = await _unitOfWork._ConversationsRepository
               .GetAllAsync(include: q => q
               .Include(c => c.messages));

       var conversation = conversations.FirstOrDefault(c => c.Id == conversationId);
           if (conversation == null) return NotFound();

         
           var ordered = conversation.messages.OrderBy(m => m.CreatedAt).ToList();

           var vm = new ChatViewModel
           {
               CurrentUserId = currentUser.Id,
               Conversation = _mapper.Map<ConversationDto>(conversation),
               Messages = _mapper.Map<List<MessageDto>>(ordered) 
           };

           return View(vm);
       }

          
       
    }
}
