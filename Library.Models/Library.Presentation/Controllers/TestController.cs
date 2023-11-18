using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
