using BackendAPI.Models;
using FrontEnd_View.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace FrontEnd_View.Controllers
{
    public class OutgoingController : Controller
    {
        //this cntroler is for incoming order and vendor details 
        Uri baseAddress = new Uri("http://localhost:39496/api");
        private readonly HttpClient _client;

        public OutgoingController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index() //for showing customer list 
        {
            string staffNameJson = HttpContext.Session.GetString("SideBarNameList");
            string staffType = HttpContext.Session.GetString("roletype");
            List<string> sideBarNameList = JsonConvert.DeserializeObject<List<string>>(staffNameJson);
            if (sideBarNameList.Contains("Customer") || staffType == "Admin")
            {

                int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
                int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
                List<Customer> customer = new List<Customer>();
                HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                    "/Customer/Get?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<List<Customer>>(data);
                }

                return View(customer);
            }
            else
            {
                return RedirectToAction("Error", "Shared");
            }

        }

        public IActionResult CustomerAdd(Customer cust) //customer add
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            cust.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(cust);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                "/Customer/AddCustomer?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public IActionResult CustomerEdit(Customer cust) //customer edit
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            cust.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(cust);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                "/Customer/UpdateCustomer?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public IActionResult CustomerDelete(int Id)  //customer delete
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            HttpResponseMessage responseMessage = _client.DeleteAsync(_client.BaseAddress +
               "/Customer/DeleteCustomer?id=" + Id + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }

        public IActionResult OutOrder()  //show orders
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            List<Customer> customer = new List<Customer>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Customer/Get?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<List<Customer>>(data);
                var customerlist = customer.Select(s => new { name = s.Name, id = s.Id }).ToList();
                ViewBag.customerListData = customerlist;
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

        public IActionResult AddOutOrders(Out_OrderRequest ord)
        {

            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            ord.order.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(ord);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                "/Out_Order/AddOrder?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public IActionResult GetOutOrders()
        {
            string staffNameJson = HttpContext.Session.GetString("SideBarNameList");
            string staffType = HttpContext.Session.GetString("roletype");
            List<string> sideBarNameList = JsonConvert.DeserializeObject<List<string>>(staffNameJson);
            if (sideBarNameList.Contains("Outgoing Orders") || staffType == "Admin")
            {

                int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
                int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
                List<Customer> customer = new List<Customer>();
                HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                    "/Customer/Get?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    customer = JsonConvert.DeserializeObject<List<Customer>>(data);
                    var customerlist = customer.Select(s => new { name = s.Name, id = s.Id }).ToList();
                    ViewBag.customerListData = customerlist;
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
                HttpResponseMessage responseMessage2 = _client.GetAsync(_client.BaseAddress +
                    "/Out_Order/GetOrderInfo?OrgId=" + OrgId + "&StaffId=" + StaffId).Result;

                if (responseMessage2.IsSuccessStatusCode)
                {
                    string data = responseMessage2.Content.ReadAsStringAsync().Result;
                    incord = JsonConvert.DeserializeObject<List<dynamic>>(data);
                }

                return View(incord);
            }
            else
            {
                return RedirectToAction("Error", "Shared");
            }

        }

        public object GetOrderInfoBySellOrderId(string Id)
        {
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            List<SellInfoById> incord = new List<SellInfoById>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Out_Order/GetOrderInfoBySalesOrderId?SalesOrderID=" + Id + "&StaffId=" + StaffId).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                incord = JsonConvert.DeserializeObject<List<SellInfoById>>(data);
            }

            return incord;

        }  //side view order

        public IActionResult UpdateOutOrders(Out_OrderRequest ord)
        {

            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            ord.order.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(ord);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                "/Out_Order/UpdateOrder?StaffId=" + StaffId, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }   //update orders

        public IActionResult DeleteOutOrders(string sellerId)
        {

            int StaffId = HttpContext.Session.GetInt32("staffId") ?? 0;
            HttpResponseMessage responseMessage = _client.DeleteAsync(_client.BaseAddress +
                "/Out_Order/DeleteOrder?SellOrderId=" + sellerId + "&StaffId=" + StaffId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }   //update orders
    }
}
