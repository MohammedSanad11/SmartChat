using Microsoft.AspNetCore.Mvc;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Features.TypingStatuses.Commands.AddTypingStatues;
using SmartChat.Application.Features.TypingStatuses.Commands.DeleteTypingStatues;
using SmartChat.Application.Features.TypingStatuses.Commands.UpdateTypingStatues;
using SmartChat.Application.Features.TypingStatuses.Quereys.GetTypeingStatues;
using SmartChat.Application.Features.TypingStatuses.Quereys.GetTypingStatuesById;
using SmartChat.Domain.Entities.Conversations;
using SmartChat.Domain.Entities.Users;

namespace SmartChat.Web.Controllers.TypingStatuses
{
    public class TypingStatuses : Controller
    {
        private readonly ICustomMediator _mediator;

        public TypingStatuses(ICustomMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var statuses = await _mediator.Send(new GetAllTypingStatusesQuery());
            return View(statuses);
        }

        // GET: TypingStatus/Details/{id}
        public async Task<IActionResult> Details(Guid userId ,Guid conversationId)
        {
            var status = await _mediator.Send(new GetTypingStatuesByIdQuery {UserId =userId , ConversationId =conversationId });
            if (status == null)
                return NotFound();

            return View(status);
        }

     
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddTypingStatuesCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

 
        public async Task<IActionResult> Edit(Guid userId , Guid conversationId)
        {
            var status = await _mediator.Send(new GetTypingStatuesByIdQuery { UserId = userId ,ConversationId = conversationId  });
            if (status == null)
                return NotFound();

            var command = new UpdateTypingStatuesCommand
            {
                UserId = userId,
                ConversationId = conversationId
            };
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid conversationId, Guid userId, UpdateTypingStatuesCommand command)
        {
            if (conversationId != command.ConversationId || userId != command.UserId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(command);

            var success = await _mediator.Send(command);
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // GET: TypingStatus/Delete/{id}
        public async Task<IActionResult> Delete(Guid userId ,Guid conversationId)
        {
            var status = await _mediator.Send(new GetTypingStatusByConversationUserQuery
            {
                ConversationId = conversationId,
                UserId = userId
            });
            if (status == null)
                return NotFound();

            return View(status);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var success = await _mediator.Send(new DeleteTypingStatuesCommand());
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}

    
