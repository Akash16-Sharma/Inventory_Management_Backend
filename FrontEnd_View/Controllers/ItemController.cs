using Microsoft.AspNetCore.Mvc;

namespace FrontEnd_View.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
