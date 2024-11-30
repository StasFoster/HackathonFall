using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Controllers
{
    public class EntranceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
