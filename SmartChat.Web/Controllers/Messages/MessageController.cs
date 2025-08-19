using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Features.Messeges.Commands.AddMessage;
using SmartChat.Application.Features.Messeges.Commands.DeleteMassege;
using SmartChat.Application.Features.Messeges.Commands.UpdateMessage;
using SmartChat.Application.Features.Messeges.Qyerys.GetMessageById;
using SmartChat.Application.Features.Roles.Queres;
using SmartChat.Domain.Entities.Conversations;

namespace SmartChat.Web.Controllers.Messages
{
    public class MessageController : Controller
    {
        private readonly ICustomMediator _mediator;
        private readonly IMapper _mapper;

        public MessageController(ICustomMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var message = await _mediator.Send(new GetAllRoleQuery());
            return View(message);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var message = await _mediator.Send(new GetMessageByIdQuery(id));
            if (message == null)
                return NotFound();

            return View(message);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(AddMessageCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Edit(Guid id)
        {
            var message = await _mediator.Send(new GetMessageByIdQuery(id));
            if (message == null)
                return NotFound();

            var command = new UpdateMessageCommand
            {
                Id = message.Id,
                Text = message.Text
            };
            return View(command);
        }

       
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateMessageCommand command)
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
            var message = await _mediator.Send(new GetMessageByIdQuery(id));
            if (message == null)
                return NotFound();

            return View(message);
        }

       
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var success = await _mediator.Send(new DeleteMessageCommand { Id = id });
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}

