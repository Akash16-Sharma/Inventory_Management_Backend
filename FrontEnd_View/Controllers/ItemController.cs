using BackendAPI.Models;
using BackendAPI.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

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

        public IActionResult Index() //item list
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            //category assign to Viewbag 
            List<Category> categoryLists = new List<Category>();
            HttpResponseMessage responseMessage1 = _client.GetAsync(_client.BaseAddress +
                "/Category/Get?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;
            if (responseMessage1.IsSuccessStatusCode)
            {
                string data = responseMessage1.Content.ReadAsStringAsync().Result;
                categoryLists = JsonConvert.DeserializeObject<List<Category>>(data);
                var stateListData = categoryLists.Select(s => new { name = s.Name, id = s.Id }).ToList();
                ViewBag.Category = stateListData;
            }

            //unitytype assign to Viewbag 
            List<Category> unitLists = new List<Category>();
            HttpResponseMessage responseMessage2 = _client.GetAsync(_client.BaseAddress +
                "/UnitType/GetAllUnitTypes?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;
            if (responseMessage1.IsSuccessStatusCode)
            {
                string data = responseMessage2.Content.ReadAsStringAsync().Result;
                unitLists = JsonConvert.DeserializeObject<List<Category>>(data);
                var stateListData = unitLists.Select(s => new { name = s.Name, id = s.Id }).ToList();
                ViewBag.Unit = stateListData;
            }


            //return item list 
            List<dynamic> items = new List<dynamic>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Item/GetItemInfo?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<dynamic>>(data);
            }
            return View(items);
        }

        public IActionResult AddItem(Item item)
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            item.Org_Id = OrgId;

            string data = JsonConvert.SerializeObject(item);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                "/Item/AddItem?StaffId=" + StaffId, con).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });


        }

        public IActionResult ItemCategory() //category list
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;

            List<Category> category = new List<Category>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Category/Get?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<List<Category>>(data);
            }

            return View(category);
        }

        public IActionResult ItemDelete(int Id)
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            HttpResponseMessage responseMessage = _client.DeleteAsync(_client.BaseAddress +
               "/Item/DeleteItem?Id=" + Id + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });

        } //item delete by Id

        [HttpGet]
        public IActionResult ItemGetById(int Id)
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
               "/Item/GetItemById?id=" + Id + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                Item item = new Item();
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<Item>(data);
                return new JsonResult(item);

            }
            return View();

        } //item get by Id

        public IActionResult ItemCategoryAdd(Category cat) //category add
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            cat.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(cat);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                "/Category/AddCategory?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public IActionResult ItemCategoryDelete(int Id)
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;

            HttpResponseMessage responseMessage = _client.DeleteAsync(_client.BaseAddress +
               "/Category/DeleteCategory?id=" + Id + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false }); ;

        } //category delete by Id

        public IActionResult ItemCategoryEdit(Category cat) //category add
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            cat.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(cat);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                "/Category/EditCategory?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public IActionResult ItemUpdate(Item item) //category add
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            item.Org_Id = OrgId;
            string data = JsonConvert.SerializeObject(item);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                "/Item/UpdateItem?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }


    }
}
