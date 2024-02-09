using BackendAPI.Models;
using FrontEnd_View.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FrontEnd_View.Controllers
{
    public class IncomingController : Controller
    {

        //this cntroler is for incoming order and vendor details 
        Uri baseAddress = new Uri("http://localhost:39496/api");
        private readonly HttpClient _client;

        public IncomingController()
        {
            _client =new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index() //for showing vendor list 
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            List<Vendor> vendor = new List<Vendor>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Vendor/Get?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                vendor = JsonConvert.DeserializeObject<List<Vendor>>(data);
            }

            return View(vendor);

        }

        public IActionResult VendorAdd(Vendor ven) //vendor add
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            ven.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(ven);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                "/Vendor/AddVendor?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult VendorEdit(Vendor ven) //vendor edit
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            ven.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(ven);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                "/Vendor/UpdateVendor?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult VendorDelete(int Id)  //vendor delete
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            HttpResponseMessage responseMessage = _client.DeleteAsync(_client.BaseAddress +
               "/Vendor/DeleteVendor?id=" + Id + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        } 

        public IActionResult IncOrders()  //show orders
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            List<Vendor> vendor = new List<Vendor>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Vendor/Get?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                vendor = JsonConvert.DeserializeObject<List<Vendor>>(data);
                var vendorlist = vendor.Select(s => new { name = s.Name, id = s.Id }).ToList();
                ViewBag.VendorListData = vendorlist;
            }

            List<dynamic> items = new List<dynamic>();
            HttpResponseMessage responseMessage2 = _client.GetAsync(_client.BaseAddress +
                "/Item/GetItemInfo?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;
            if (responseMessage2.IsSuccessStatusCode)
            {
                string data = responseMessage2.Content.ReadAsStringAsync().Result;
                items = JsonConvert.DeserializeObject<List<dynamic>>(data);
              //  var itemlist = items.Select(s => new { name = s.name, id = s.id, opening_Stock = s.opening_Stock }).ToList();
                var itemlists = items.Select(s => new { name = s.name, id = s.id, opening_Stock = s.opening_Stock }).ToList();

                ViewBag.ItemLists = itemlists;
            }

            return View();
        }


        public IActionResult AddIncOrders([FromBody] FullIncOrder ord)
        {

            return View();
        } 

    }
}
