using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectBlog.DAL.Entities;
using ProjectBlog.DAL.Repositories;

namespace ProjectBlog.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRepository<Role> _repo;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRepository<Role> repo, ILogger<RoleController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к RoleController");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _repo.GetAll();
            _logger.LogInformation("RoleController - Index");
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _repo.Get(id);
            _logger.LogInformation("RoleController - GetRoleById");
            return View(role);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            _logger.LogInformation("RoleController - Add");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(Role newRole)
        {
            await _repo.Add(newRole);
            _logger.LogInformation("RoleController - Add - complete");
            return View(newRole);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _repo.Get(id);
            await _repo.Delete(role);
            _logger.LogInformation("RoleController - Delete");
            return RedirectToAction("Index", "Roles");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var role = await _repo.Get(id);
            _logger.LogInformation("RoleController - Update");
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmUpdating(Role role)
        {
            await _repo.Update(role);
            _logger.LogInformation("RoleController - Update - confirm");
            return RedirectToAction("Index", "Roles");
        }
    }
}
