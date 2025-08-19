using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Users;

namespace SmartChat.Web.Controllers.Conversations
{
    public class ConversationController : Controller
    {
        private readonly ICustomMediator _mediator;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IUintOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConversationController(ICustomMediator mediator, IHubContext<ChatHub> chatHubContext, IUintOfWork unitOfWork, IMapper mapper)
        {
            _mediator = mediator;
            _chatHubContext = chatHubContext;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var users = await _unitOfWork._UsersRepository.GetAllAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return View(userDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid selectedUserId)
        {
            var username = User.Identity?.Name;
            var currentUser = await _unitOfWork._UsersRepository
                .GetByConditionAsync(u => u.UesrName == username);

            if (currentUser == null) return Unauthorized();

            var newConversation = new Conversation
            {
                Id = Guid.NewGuid(),
                UserId = currentUser.Id,
                AgentId = selectedUserId,
                CreateAt = DateTime.Now,
             };

            await _unitOfWork._ConversationsRepository.AddAsync(newConversation);
            await _unitOfWork.SaveChangeAsync();

            return RedirectToAction("Dashboard", "User");
        }
    }
}
