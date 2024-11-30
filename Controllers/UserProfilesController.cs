using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Controllers
{
    public class UserProfilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
