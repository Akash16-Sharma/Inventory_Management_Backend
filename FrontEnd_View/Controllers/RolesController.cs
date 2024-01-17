using Microsoft.AspNetCore.Mvc;

namespace FrontEnd_View.Controllers
{
    public class RolesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
