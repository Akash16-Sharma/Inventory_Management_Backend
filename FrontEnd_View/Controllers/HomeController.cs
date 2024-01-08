using Microsoft.AspNetCore.Mvc;

namespace FrontEnd_View.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
