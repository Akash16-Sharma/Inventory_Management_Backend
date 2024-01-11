using Microsoft.AspNetCore.Mvc;

namespace FrontEnd_View.Controllers
{
    public class ItemController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:39496/api");
        private readonly HttpClient _client;

        public ItemController()
        {
                _client = new HttpClient(); 
            _client.BaseAddress = baseAddress;
        }


        public IActionResult Index()
        {

            return View();
        }
    }
}
