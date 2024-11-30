using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
