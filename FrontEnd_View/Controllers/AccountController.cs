using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using System.Security;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Reflection.Metadata.Ecma335;


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


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckLogin( User_Login log)
        {
            try
            {
                string data = JsonConvert.SerializeObject(log);
                StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                    "/Account/login", con).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                    int orgId = responseObject.value.orgId;
                    int staffId = responseObject.value.staffId;
                    string orgname=  responseObject.value.orgName;
                    HttpContext.Session.SetInt32("orgId", orgId);
                    HttpContext.Session.SetInt32("staffId", staffId);
                    HttpContext.Session.SetString("orgname", orgname);
                    return Json(new { success = true, redirectUrl = Url.Action("ItemCategory", "Item") });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
            return Json(new { success = false });
        }

        [HttpGet]
        public IActionResult Signup()
        {
           //assign states to Viewbag
           List<StateList> stateLists = new List<StateList>();  
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Account/Statelist").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data= responseMessage.Content.ReadAsStringAsync().Result;
                stateLists =JsonConvert.DeserializeObject<List<StateList>>(data);
                var stateListData = stateLists.Select(s => new { name = s.Name, id = s.Id }).ToList();
                ViewBag.StateListData = stateListData;
                return View();
            }

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
                    return RedirectToAction("Login");
                }

                return View();
            }
            catch (Exception ex)
            {
                return View();
                throw;
            }
        }

  
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Account");

        }


    }
}
