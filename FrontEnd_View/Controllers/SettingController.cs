using Microsoft.AspNetCore.Mvc;

namespace FrontEnd_View.Controllers
{
    public class SettingController : Controller
    {
        //this conttrolerr is for setting Page where user info nd org info show
        Uri baseAddress = new Uri("http://localhost:39496/api");
        private readonly HttpClient _client;

        public SettingController()
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
