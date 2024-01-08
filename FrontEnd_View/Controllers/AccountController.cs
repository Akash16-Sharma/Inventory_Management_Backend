using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using System.Security;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;

namespace FrontEnd_View.Controllers
{
    public class AccountController : Controller
    {

        Uri baseAddress = new Uri("http://localhost:39496/api");
        private readonly HttpClient _client;

        public AccountController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckLogin([FromBody] User_Login log)
        {
            try
            {
                string data = JsonConvert.SerializeObject(log);
                StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                    "/Account/login", con).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Signup", "Account");
                }

                return View();
            }
            catch (Exception ex)
            {
                return View();
                throw;
            }
        }

        [HttpGet]
        public IActionResult Signup()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Signup([FromBody] AddAllInfo info)
        {
            try
            {
                string data = JsonConvert.SerializeObject(info);
                StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                    "/Account/AddOrganisation", con).Result;
                    
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                return View();
                throw;
            }
        }

    }
}
