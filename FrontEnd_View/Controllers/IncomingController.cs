using BackendAPI.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using FrontEnd_View.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;



namespace FrontEnd_View.Controllers
{
    public class IncomingController : Controller
    {

        //this cntroler is for incoming order and vendor details 
        Uri baseAddress = new Uri("http://localhost:39496/api");
        private readonly HttpClient _client;

        public IncomingController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index() //for showing vendor list 
        {
            string staffNameJson = HttpContext.Session.GetString("SideBarNameList");
            string staffType = HttpContext.Session.GetString("roletype");
            List<string> sideBarNameList = JsonConvert.DeserializeObject<List<string>>(staffNameJson);
            if (sideBarNameList.Contains("Vendor") || staffType == "Admin")
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
            else
            {
                return RedirectToAction("Error", "Shared");
            }

        }

        public IActionResult VendorAdd(Vendor ven) //vendor add
        {
            ven.Email ??= "";
            ven.Phone ??= "";
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            ven.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(ven);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                "/Vendor/AddVendor?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public IActionResult VendorEdit(Vendor ven) //vendor edit
        {

            ven.Email ??= "";
            ven.Phone ??= "";
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            ven.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(ven);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                "/Vendor/UpdateVendor?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public IActionResult VendorDelete(int Id)  //vendor delete
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            HttpResponseMessage responseMessage = _client.DeleteAsync(_client.BaseAddress +
               "/Vendor/DeleteVendor?id=" + Id + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });

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

        public IActionResult AddIncOrders(IncOrderRequest ord)
        {

            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            ord.Inc_Orders.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(ord);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                "/Inc_Order/AddOrder?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult GetIncORders()
        {

            string staffNameJson = HttpContext.Session.GetString("SideBarNameList");
            string staffType = HttpContext.Session.GetString("roletype");
            List<string> sideBarNameList = JsonConvert.DeserializeObject<List<string>>(staffNameJson);

            if (sideBarNameList.Contains("Incoming Orders") || staffType == "Admin")
            {
                int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
                int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
                List<Vendor> vendor = new List<Vendor>();
                HttpResponseMessage responseMessage2 = _client.GetAsync(_client.BaseAddress +
                    "/Vendor/Get?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

                if (responseMessage2.IsSuccessStatusCode)
                {
                    string data = responseMessage2.Content.ReadAsStringAsync().Result;
                    vendor = JsonConvert.DeserializeObject<List<Vendor>>(data);
                    var vendorlist = vendor.Select(s => new { name = s.Name, id = s.Id }).ToList();
                    ViewBag.VendorListData = vendorlist;
                }

                List<dynamic> items = new List<dynamic>();
                HttpResponseMessage itemresponseMessage = _client.GetAsync(_client.BaseAddress +
                    "/Item/GetItemInfo?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

                if (itemresponseMessage.IsSuccessStatusCode)
                {
                    string data = itemresponseMessage.Content.ReadAsStringAsync().Result;
                    items = JsonConvert.DeserializeObject<List<dynamic>>(data);
                    var itemlist = items.Select(s => new SelectListItem { Text = s.name, Value = s.id.ToString() }).ToList();
                    ViewBag.ItemListData = itemlist;

                }


                List<dynamic> incord = new List<dynamic>();
                HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                    "/Inc_Order/GetOrderInfo?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    incord = JsonConvert.DeserializeObject<List<dynamic>>(data);
                }

                return View(incord);
            }
            else
            {
                return RedirectToAction("Error", "Shared");
            }

        }

        public object GetOrderInfoByPurchaseOrderId(string Id)
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            List<PurchaseInfoById> incord = new List<PurchaseInfoById>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Inc_Order/GetOrderInfoByPurchaseOrderId?PurchaseOrderId=" + Id + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                incord = JsonConvert.DeserializeObject<List<PurchaseInfoById>>(data);
            }

            return incord;

        }  //side view order

        public IActionResult UpdateIncOrders(IncOrderRequest ord)
        {

            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            ord.Inc_Orders.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(ord);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                "/Inc_Order/UpdateOrder?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }   //update orders

        public IActionResult DeleteIncOrders(string PurID)
        {
         
           int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            HttpResponseMessage responseMessage = _client.DeleteAsync(_client.BaseAddress +
                "/Inc_Order/DeleteOrder?PurchaseOrderId=" + PurID + "&StaffId=" + StaffId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }   //update orders
    }
}
