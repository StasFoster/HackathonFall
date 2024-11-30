using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Controllers
{
    public class FindfriendsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
