using Microsoft.AspNetCore.Mvc;

namespace ProjectBlog.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Forbidden()
        {
            return View();
        }

        public IActionResult Resource()
        {
            return View();
        }

        public IActionResult GoWrong()
        {
            return View();
        }
    }
}
