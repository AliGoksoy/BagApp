using Microsoft.AspNetCore.Mvc;

namespace BagApp.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Intro()
        {
            return View();
        }
    }
}
