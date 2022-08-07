using Microsoft.AspNetCore.Mvc;

namespace Suggestion.Server.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
