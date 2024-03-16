using BackendAPI.Models;
using BackendAPI.Models.Roles;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;

            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
            "/Account/GetOrganisation?OrgId=" + OrgId).Result; 

            HttpResponseMessage responseMessageInfo = _client.GetAsync(_client.BaseAddress +
            "/Settings/GetUserInfo?orgid=" + OrgId + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                Organisation_Info orginfo = new Organisation_Info();
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                orginfo = JsonConvert.DeserializeObject<Organisation_Info>(data);

                if (responseMessageInfo.IsSuccessStatusCode)
                {
                    User_Info userinfo = new User_Info();
                    string datas = responseMessageInfo.Content.ReadAsStringAsync().Result;
                    userinfo = JsonConvert.DeserializeObject<User_Info>(datas);

                    //  return View((orginfo, userinfo));
                    return View(new Tuple<Organisation_Info, User_Info>(orginfo, userinfo));

                }
                return View();

            }
            else
                return View();

        }

        [HttpGet]
        public JsonResult GetCities(int Id)
        {

            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Account/Citylist?Stateid=" + Id).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                List<Cities_List> citiesLists = new List<Cities_List>();
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                citiesLists = JsonConvert.DeserializeObject<List<Cities_List>>(data);
                return new JsonResult(citiesLists);

            }
            else
                return new JsonResult("null");
        }


        [HttpPost]
        public IActionResult UpdateOrg([FromBody] Organisation_Info info) //setting>update org 
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            try
            {
                string data = JsonConvert.SerializeObject(info);
                StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                    "/Settings/UpdateOrg?StaffId=" + StaffId, con).Result;

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


        [HttpPost]
        public IActionResult AddInfo([FromBody]User_Info info)
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            try
            {
                string data = JsonConvert.SerializeObject(info);
                StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                    "/Settings/AddUserInfo?StaffId=" + StaffId, con).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateInfo([FromBody] User_Info info)
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            try
            {
                string data = JsonConvert.SerializeObject(info);
                StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                    "/Settings/UpdateUserInfo?StaffId=" + StaffId, con).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
                throw;
            }
        }

    }

}
