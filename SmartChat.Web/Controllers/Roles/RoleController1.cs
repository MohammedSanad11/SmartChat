using Microsoft.AspNetCore.Mvc;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Features.Roles.Commands.AddRole;
using SmartChat.Application.Features.Roles.Commands.DeleteRole;
using SmartChat.Application.Features.Roles.Commands.UpdateRole;
using SmartChat.Application.Features.Roles.Queres;
using SmartChat.Application.Features.Roles.Queres.GetRoleById;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SmartChat.Web.Controllers.Roles
{
    public class RoleController1 : Controller
    {
        private readonly ICustomMediator _mediator;

        public RoleController1(ICustomMediator mediator)
        {
            _mediator = mediator;
        }

        public  async Task<IActionResult> Index()
        {
            var roles = await _mediator.Send(new GetAllRoleQuery());
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddCommandRole addCommand)
        {

            if (!ModelState.IsValid)
                return View(addCommand);

            await _mediator.Send(addCommand);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Eidt(Guid Id)
        {
            var role = await _mediator.Send(new GetRoleByIdQuery { Id = Id });
            if (role == null)
                return NotFound();

            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UpdateCommandRole command)
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
            var role = await _mediator.Send(new GetRoleByIdQuery { Id = id });
            if (role == null)
                return NotFound();

            return View(role);
        }
        // POST: Role/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var success = await _mediator.Send(new DeleteRoleCommand { Id = id });
            if (!success)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }

}


