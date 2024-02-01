using BackendAPI.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            List<StateList> stateLists = new List<StateList>();
            HttpResponseMessage responseMessage2 = _client.GetAsync(_client.BaseAddress +
                "/Account/Statelist").Result;
            if (responseMessage2.IsSuccessStatusCode)
            {
                string data = responseMessage2.Content.ReadAsStringAsync().Result;
                stateLists = JsonConvert.DeserializeObject<List<StateList>>(data);
                var stateListData = stateLists.Select(s => new { name = s.Name, id = s.Id }).ToList();
                ViewBag.StateListData = stateListData;
            }

                int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
                HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Account/GetOrganisation?OrgId=" + OrgId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                List<Organisation_Info> orginfo = new List<Organisation_Info>();
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                orginfo = JsonConvert.DeserializeObject<List<Organisation_Info>>(data);
                return View(orginfo);

            }
            else
                return View();

        }
    }
}
