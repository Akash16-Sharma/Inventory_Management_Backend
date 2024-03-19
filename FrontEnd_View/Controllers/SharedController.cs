using Microsoft.AspNetCore.Mvc;

namespace FrontEnd_View.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
