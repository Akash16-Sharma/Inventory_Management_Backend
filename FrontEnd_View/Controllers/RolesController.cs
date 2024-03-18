using BackendAPI.Models;
using BackendAPI.Models.Roles;
using FrontEnd_View.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace FrontEnd_View.Controllers
{
    public class RolesController : Controller
    {
        //this cntroler is for ROLES and vendor details 

        Uri baseAddress = new Uri("http://localhost:39496/api");
        private readonly HttpClient _client;

        public RolesController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index() //add staff nd roles
        { 
            return View();
        }

        public IActionResult AddStaff([FromBody] StaffRoleRequest staffRoleRequest)
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            staffRoleRequest.Staff.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(staffRoleRequest);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PostAsync(_client.BaseAddress +
                "/Roles/AddStaff?FromEmail=" + staffRoleRequest.Staff.Email, con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }

        public IActionResult List()
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;

            List<Staff> stafflist = new List<Staff>();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Roles/GetStaff?OrgId=" + OrgId).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                stafflist = JsonConvert.DeserializeObject<List<Staff>>(data);  
            }
            return View(stafflist);
        }

        public object GetAccessById(int Id)
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;

            RolesGet stafflist = new RolesGet();
            HttpResponseMessage responseMessage = _client.GetAsync(_client.BaseAddress +
                "/Roles/GetAccessById?StaffId=" + Id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                stafflist = JsonConvert.DeserializeObject<RolesGet>(data);
            }
            return stafflist;

        }

        public IActionResult UpdateStaff([FromBody] StaffRoleRequest staffRoleRequest)
        {
            int OrgId = HttpContext.Session.GetInt32("orgId") ?? 0;
            staffRoleRequest.Staff.OrgId = OrgId;
            string data = JsonConvert.SerializeObject(staffRoleRequest);
            StringContent con = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = _client.PutAsync(_client.BaseAddress +
                "/Roles/UpdateStaff", con).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }

        public IActionResult DeleteStaff(int Id)
        {
            HttpResponseMessage responseMessage = _client.DeleteAsync(_client.BaseAddress +
                "/Roles/DeleteStaff?DelId="+Id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }

    }
}
