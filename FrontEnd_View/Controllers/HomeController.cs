using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd_View.Controllers
{
    public class HomeController : Controller
    {
        //this cntroler is for dashboard 

        Uri baseAddress = new Uri("http://localhost:39496/api");
        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()   //dashboard 
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            List<Item> vendor = new List<Item>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Dashboard/LowStock?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                vendor = JsonConvert.DeserializeObject<List<Item>>(data);
                var vendorlist = vendor.Select(s => new { name = s.Name, id = s.Id, stock=s.Opening_Stock }).ToList();
                ViewBag.VendorListData = vendorlist;
            }

            return View(); 
        }
    }
}
