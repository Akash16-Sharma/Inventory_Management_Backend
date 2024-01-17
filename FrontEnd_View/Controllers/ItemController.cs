using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            //category assign to Viewbag 
            List<Category> categoryLists = new List<Category>();
            HttpResponseMessage responseMessage1 = _client.GetAsync(_client.BaseAddress +
                "/Category/Get").Result;
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
                "/UnitType/GetAllUnitTypes").Result;
            if (responseMessage1.IsSuccessStatusCode)
            {
                string data = responseMessage2.Content.ReadAsStringAsync().Result;
                unitLists = JsonConvert.DeserializeObject<List<Category>>(data);
                var stateListData = unitLists.Select(s => new { name = s.Name, id = s.Id }).ToList();
                ViewBag.Unit = stateListData;
            }

            //vendorassign to Viewbag 
            List<Category> vendorLists = new List<Category>();
            HttpResponseMessage responseMessage3 = _client.GetAsync(_client.BaseAddress +
                "/Vendor/Get").Result;
            if (responseMessage1.IsSuccessStatusCode)
            {
                string data = responseMessage3.Content.ReadAsStringAsync().Result;
                vendorLists = JsonConvert.DeserializeObject<List<Category>>(data);
                var vendorListData = vendorLists.Select(s => new { name = s.Name, id = s.Id }).ToList();
                ViewBag.Vendor = vendorListData;
            }


            //return item list 
            List<dynamic> items = new List<dynamic>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Item/GetItemInfo?orgid="+1).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<dynamic>>(data);
            }
            return View(items);
        }

        [HttpPost]
        public IActionResult AddItem(Item item)
        {
           item.Org_Id = 1; //deafault set 
            try
            {
                string data = JsonConvert.SerializeObject(item);
                StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                    "/Item/AddItem", con).Result;

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

        public IActionResult ItemCategory() //category list
        {
            List<Category> category = new List<Category>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Category/Get").Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                category = JsonConvert.DeserializeObject<List<Category>>(data);
            }

            return View(category);
        }

        public IActionResult ItemCategoryAdd(Category cat) //category add
        {
            string data = JsonConvert.SerializeObject(cat);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                "/Category/AddCategory", con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ItemCategory");
            }

            return View();
        }

    }
}
