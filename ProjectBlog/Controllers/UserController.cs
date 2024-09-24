using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using ProjectBlog.Models;
using ProjectBlog.DAL.Repositories;
using System.Security.Authentication;
using System.Security.Claims;
using ProjectBlog.DAL.Entities;
namespace ProjectBlog.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository<User> _repo;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IRepository<User> repo, ILogger<UsersController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к UsersController");
        }

        [HttpGet]
        [Route("Authenticate")]
        public IActionResult Authenticate()
        {
            _logger.LogInformation("UsersController - Authenticate");
            return View();
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(string email, string password)
        {
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Некорректные данные");

            User user = _repo.GetByLogin(email);
            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль некорректен");

            string role = user.RoleId.ToString();

            List<Claim> userClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Email),
                            new Claim(ClaimTypes.Role, role),
                        };

            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            _logger.LogInformation("UsersController - Authenticate - complete");
            return View("Profile", user);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("UsersController - LogOut");
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _repo.GetAll();
            _logger.LogInformation("UsersController - Index");
            return View(users);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _repo.Get(id);
            _logger.LogInformation("UsersController - GetUserById");
            return View(user);
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            _logger.LogInformation("UsersController - Register");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public async Task<IActionResult> Register([Bind("Id,FirstName,LastName,Email,Password")] User newUser)
        {
            if (ModelState.IsValid)
            {
                await _repo.Add(newUser);
                _logger.LogInformation("UsersController - Register - complete");
                return View(newUser);

            }
            return View(newUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Update")]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _repo.Get(id);
            _logger.LogInformation("UsersController - Update");
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmUpdating([Bind("Id,FirstName,LastName,Email,Password")] User user)
        {
            await _repo.Update(user);
            _logger.LogInformation("UsersController - Update - complete");
            return RedirectToAction("Index", "Users");
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _repo.Get(id);
            if (user != null)
            {
                await _repo.Delete(user);
            }
            _logger.LogInformation("UsersController - Delete");
            return RedirectToAction("Index", "Users");
        }
    }
}
