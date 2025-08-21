using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Features.Users.Commands.AddUsers;
using SmartChat.Application.Features.Users.Commands.DeleteUsers;
using SmartChat.Application.Features.Users.Commands.UpdateUsers;
using SmartChat.Application.Features.Users.Queres.GetAllUsers;
using SmartChat.Application.Features.Users.Queres.GetUsersById;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Web.Mapping;
using SmartChat.Web.Views.viewModle;

namespace SmartChat.Web.Controllers.Users
{
    public class UserController : Controller
    {
        private readonly ICustomMediator _mediator;
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public UserController(ICustomMediator mediator, IUintOfWork uintOfWork)
        {
            _mediator = mediator;
            _uintOfWork = uintOfWork;
        }

        public async Task<IActionResult> Dashboard()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Auth");

            var currentUser = await _uintOfWork._UsersRepository.GetByConditionAsync(u => u.UesrName == username);
            if (currentUser == null) return NotFound();

            var role = await _uintOfWork._RolesRepository.GetByConditionAsync(r => r.Id == currentUser.RoleId);
            ViewData["RoleName"] = role?.Name ?? "Unknown";

            // ===== My Conversations =====
            var myConversations = await _uintOfWork._ConversationsRepository.FindAsync(
                c => c.UserId == currentUser.Id || c.AgentId == currentUser.Id,
                q => q.Include(c => c.messages)
                      .Include(c => c.User)
                      .Include(c => c.Agent)
            );

            // ===== All Conversations (Admin only) =====
            IEnumerable<Conversation> allConversations = null;
            if (role?.Name == "Admin")
            {
                allConversations = await _uintOfWork._ConversationsRepository.FindAsync(
                    c => true,
                    q => q.Include(c=>c.messages)
                          .Include(c => c.User)
                          .Include(c => c.Agent)
                );
            }

            // ===== Build My Conversations VM =====
            var myDashboardVm = new List<UserDashboardViewModel>();
            foreach (var c in myConversations)
            {
                var lastMessage = c.messages?.OrderByDescending(m => m.CreatedAt).FirstOrDefault();

                string conversationTitle = "";
                if (c.UserId == currentUser.Id && c.Agent != null)
                    conversationTitle = c.Agent.Name;
                else if (c.AgentId == currentUser.Id && c.User != null)
                    conversationTitle = c.User.Name;
                else if (c.User != null && c.Agent != null)
                    conversationTitle = c.User.Name + " & " + c.Agent.Name;
                else
                    conversationTitle = "Unknown";

                string senderName = "Unknown";
                if (lastMessage?.SenderId != null)
                {
                    var senderUser = await _uintOfWork._UsersRepository.GetByConditionAsync(u => u.Id == lastMessage.SenderId);
                    senderName = senderUser?.Name ?? "Unknown";
                }

                myDashboardVm.Add(new UserDashboardViewModel
                {
                    ConversationId = c.Id,
                    ConversationTitle = conversationTitle,
                    CreatedAt = c.CreateAt,
                    LastMessageText = lastMessage?.Text ?? "",
                    LastMessageSenderName = senderName,
                    LastMessageAt = lastMessage?.CreatedAt,
                    MessageCount = c.messages?.Count() ?? 0
                });
            }

            // ===== Build All Conversations VM for Admin (Hidden content) =====
            var allDashboardVm = new List<UserDashboardViewModel>();
            if (allConversations != null)
            {
                foreach (var c in allConversations)
                {
                    var lastMessage = c.messages?.OrderByDescending(m => m.CreatedAt).FirstOrDefault();

                    string senderName = "Unknown";
                    if (lastMessage?.SenderId != null)
                    {
                        var senderUser = await _uintOfWork._UsersRepository.GetByConditionAsync(u => u.Id == lastMessage.SenderId);
                        senderName = senderUser?.Name ?? "Unknown";
                    }

                    allDashboardVm.Add(new UserDashboardViewModel
                    {
                        ConversationId = c.Id,
                        ConversationTitle = (c.User?.Name ?? "Unknown") + " & " + (c.Agent?.Name ?? "Unknown"),
                        CreatedAt = c.CreateAt,
                        LastMessageText = "Hidden", // نخفي محتوى الرسالة
                        LastMessageSenderName = senderName,
                        LastMessageAt = lastMessage?.CreatedAt,
                        MessageCount = c.messages?.Count() ?? 0
                    });
                }
            }

            // ===== All Users for Start Chat =====
            var allUsers = await _uintOfWork._UsersRepository.FindAsync(u => u.Id != currentUser.Id);

            var vm = new DashboardViewModel
            {
                CurrentUser = new CurrentUserVm
                {
                    Id = currentUser.Id,
                    Name = currentUser.Name,
                    Role = role?.Name ?? "Unknown",
                    ConversationCount = myDashboardVm.Count
                },
                MyConversations = myDashboardVm,
                AllConversations = allDashboardVm,
                AllUsers = allUsers.Select(u => new UserVm { Id = u.Id, Name = u.Name }).ToList()
            };

            return View(vm);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            if (user == null)
                return NotFound();

            return View(user);
        }

   
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(AddCommandUser command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var id = await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            if (user == null)
                return NotFound();

            var command = new UpdateCommandUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                //Password
            };
            return View(command);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateCommandUser command)
        {
            if (id != command.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(command);

            var success = await _mediator.Send(command);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            if (user == null)
                return NotFound();

            return View(user);
        }

  
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var success = await _mediator.Send(new DeleteCommandUser { Id = id });
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
