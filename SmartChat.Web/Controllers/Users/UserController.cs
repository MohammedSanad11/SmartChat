using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Features.Users.Commands.AddUsers;
using SmartChat.Application.Features.Users.Commands.DeleteUsers;
using SmartChat.Application.Features.Users.Commands.UpdateUsers;
using SmartChat.Application.Features.Users.Queres.GetAllUsers;
using SmartChat.Application.Features.Users.Queres.GetUsersById;
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

            var user = await _uintOfWork._UsersRepository.GetByConditionAsync(u => u.UesrName == username);
            if (user == null) return NotFound();

            var userId = user.Id;

          
            var conversations = await _uintOfWork._ConversationsRepository.FindAsync(
                c => c.UserId == userId || c.AgentId == userId,
                include: q => q.Include(c => c.messages)
                              .Include(c => c.User)
                              .Include(c => c.Agent)
            );

            if (!conversations.Any())
            {
                return RedirectToAction("Create", "Conversation");
            }

            var dashboardVm = conversations
                .Select(c => new UserDashboardViewModel
                {
                    ConversationId = c.Id,
                    ConversationTitle = c.User != null ? c.User.Name : "Unknown",
                    CreatedAt = c.CreateAt,
                    LastMessageAt = c.messages != null && c.messages.Any() ? c.messages.Max(m => m.CreatedAt) : (DateTime?)null,
                    LastMessageText = c.messages != null && c.messages.Any() ?
                                      c.messages.OrderByDescending(m => m.CreatedAt).First().Text : ""
                }).ToList();

            return View(dashboardVm);
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
