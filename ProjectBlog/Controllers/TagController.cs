using Microsoft.AspNetCore.Mvc;
using ProjectBlog.DAL.Entities;
using ProjectBlog.DAL.Repositories;

namespace ProjectBlog.Controllers
{
    public class TagController : Controller
    {
        private readonly IRepository<Tag> _repo;
        private readonly ILogger<TagController> _logger;

        public TagController(IRepository<Tag> repo, ILogger<TagController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к TagController");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tags = await _repo.GetAll();
            _logger.LogInformation("TagController - Index");
            return View(tags);
        }

        [HttpGet]
        public IActionResult GetTagById()
        {
            _logger.LogInformation("TagController - GetTagById");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _repo.Get(id);
            _logger.LogInformation("TagController - GetTagById - complete");
            return View(tag);
        }

        [HttpGet]
        public IActionResult AddTag()
        {
            _logger.LogInformation("TagController - Add");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTag(Tag newTag)
        {
            await _repo.Add(newTag);
            _logger.LogInformation("TagController - Add - complete");
            return View(newTag);
        }

        [HttpPost]
       
        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _repo.Get(id);
            await _repo.Delete(tag);
            _logger.LogInformation("TagController - Delete");
            return RedirectToAction("Index", "Tags");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var tag = await _repo.Get(id);
            _logger.LogInformation("TagController - Update");
            return View(tag);
        }

        [HttpPost]
       
        public async Task<IActionResult> ConfirmUpdating(Tag tag)
        {
            await _repo.Update(tag);
            _logger.LogInformation("TagController - Update - complete");
            return RedirectToAction("Index", "Tags");
        }
    }
}
