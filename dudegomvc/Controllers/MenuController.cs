using Microsoft.AspNetCore.Mvc;

namespace dudegomvc.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
