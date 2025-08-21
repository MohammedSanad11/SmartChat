using Microsoft.AspNetCore.Mvc;
using SmartChat.Web.Views.viewModle;
using System.IdentityModel.Tokens.Jwt;      
using System.Security.Claims;               
using Microsoft.IdentityModel.Tokens;       
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace SmartChat.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUintOfWork _uintOfWork;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration, IUintOfWork uintOfWork, IMapper mapper)
        {
            _configuration = configuration;
            _uintOfWork = uintOfWork;
            this._mapper = mapper;
        }


        [HttpGet]
        public IActionResult Login() => View("Login");

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _uintOfWork._UsersRepository.GetByConditionAsync(
                u => u.Email == model.Email,
                include: q => q.Include(u => u.Role)
            );

            if (user == null || user.PassWord != model.Password)
            {
                ModelState.AddModelError("", "اسم المستخدم أو كلمة السر خاطئة");
                return View(model);
            }

               var claims = new List<Claim>
               {
                   new Claim(ClaimTypes.Name, user.UesrName),
                   new Claim(ClaimTypes.Role, user.Role.Name)
               };

        
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            var token = GenerateJwt(user);
            HttpContext.Session.SetString("JWToken", token);
            foreach (var claim in claimsPrincipal.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }


            return RedirectToAction("Dashboard", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {

            var model = new RegisterViewModel();

           
            var roles = await _uintOfWork._RolesRepository.GetAllAsync();
            model.Roles = _mapper.Map<List<RoleDto>>(roles);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var errors = string.Join("; ", ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(e => e.ErrorMessage));
                Console.WriteLine("ModelState errors: " + errors);
                return View(model);

            }
            var existingUser = await _uintOfWork._UsersRepository
                .GetByConditionAsync(u => u.UesrName == model.Username);

            if (existingUser != null)
            {
                ModelState.AddModelError("", "اسم المستخدم موجود مسبقاً");
                return View(model);
            }


            if (model.RoleId == Guid.Empty)
            {
                ModelState.AddModelError("", "يرجى اختيار الدور");
                return View(model);
            }

            var roleEntity = await _uintOfWork._RolesRepository
                .GetByConditionAsync(r => r.Id == model.RoleId);

            if (roleEntity == null)
            {
                ModelState.AddModelError("", "الدور غير موجود");
                return View(model);
            }

            model.Name = model.Username;
        
            var userDto = _mapper.Map<UserDto>(model);
            userDto.RoleName = roleEntity.Name;
            userDto.CreatedAt = DateTime.UtcNow;

           
            var user = _mapper.Map<User>(userDto);
            user.Id = Guid.NewGuid();      
            user.RoleId = roleEntity.Id;   
            user.UesrName = model.Username;

            await _uintOfWork._UsersRepository.AddAsync(user);
            await _uintOfWork.SaveChangeAsync();

            return RedirectToAction("Login");
        }
        [Authorize]
        public IActionResult Profile()
        {
            var username = User.Identity?.Name;
            return Content($"مرحباً {username}, هذه الصفحة محمية");
        }

       
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return Content("هذه لوحة الإدارة للأدمن فقط");
        }

        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UesrName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:SmartChat"],
                audience: _configuration["Jwt:SmartChatUsers"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

    }

}
