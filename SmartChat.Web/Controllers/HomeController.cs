using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartChat.Application.Core.Interfasces;
using SmartChat.Application.Features.LoginUser.command.testloginuser;
using SmartChat.Application.Features.Roles.Queres;
using SmartChat.Application.Features.Users.Commands.AddUsers;
using SmartChat.Web.Models;
using SmartChat.Web.Views.viewModle;
using System.Diagnostics;

namespace SmartChat.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomMediator _mediator;
        public HomeController(ILogger<HomeController> logger, ICustomMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
