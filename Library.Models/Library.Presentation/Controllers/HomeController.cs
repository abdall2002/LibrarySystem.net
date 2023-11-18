using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "Home | Index";
            return View();
        }

        [HttpGet]
        public IActionResult AboutUs()
        {
            ViewBag.Title = "Home | AboutUs";
            return View();
        }

        [HttpGet]
        [Route("/ContactUs", Name ="SiteInfo")]
        public IActionResult ContactUs()
        {
            ViewBag.Title = "Home | ContactUs";
            return View();
        }
    }
}
