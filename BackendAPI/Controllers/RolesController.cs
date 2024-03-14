using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using BackendAPI.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoles _Roles;
        private readonly IEmail _mail;
        public RolesController(IRoles roles,IEmail mail)
        {
            _Roles = roles;
            _mail = mail;
        }
        [HttpGet]
        [Route("GetStaff")]
        public IActionResult GetStaff(int OrgId)
        {
            var data= _Roles.GetStaff(OrgId);
            if (data == null)
            {
                return NotFound();
            }
            else { return Ok(data); }
        }

        [HttpPost]
        [Route("AddStaff")]
        public IActionResult AddStaff([FromBody] StaffRoleRequest Staff,string FromEmail)
        {
            string Password = Staff.Staff.Password;
            EmailModel email = new EmailModel();
            email.From = FromEmail;
            int StaffId = _Roles.AddStaff(Staff.Staff);
            if (StaffId>0 && Staff.Staff.RoleType == "Admin")
            {
                email.To = Staff.Staff.Email;
                email.Subject = "Credentials For Log In";
                // Assuming you have a Staff class with properties like Staff_Name, Email, etc.
                email.Body = $"Hello {Staff.Staff.Staff_Name},\n\n" +
                              $"Your login credentials for Ninja Inventory are as follows:\n" +
                              $"Username: {Staff.Staff.Email}\n" +
                              $"Password: {Password}\n\n" +
                              $"Please keep this information confidential and ensure that only authorized personnel have access to it.\n\n" +
                              "If you have any questions or concerns, feel free to contact our support team.\n\n" +
                              "Best regards,\n" +
                              "Ninja Inventory Team";

                _mail.SendEmailAsync(email);
                return Ok("Roles Added successfully.");
            }
                if (StaffId>0)
            {
                for (var i = 0; i < Staff.StaffAccess.Count; i++)
                {
                    Staff.StaffAccess[i].StaffId = StaffId;
                    bool IsAdded = _Roles.AddAccess(Staff.StaffAccess[i]);
                        continue;
                    
                }
                email.To = Staff.Staff.Email;
                email.Subject = "Credentials For Log In";
                email.Body = "Hello" + Staff.Staff.Staff_Name + "Your Credentials For Log In For Ninja Inventory Are Below" + Staff.Staff.Email + "And PassWord Is" + Staff.Staff.Password;
                _mail.SendEmailAsync(email);
                return Ok("Roles Added successfully.");
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPut]
        [Route("UpdateStaff")]
        public IActionResult UpdateStaff([FromBody] StaffRoleRequest staff)
        {
            int count = 0;
            bool IsUpdated=_Roles.UpdateStaff(staff.Staff);
           
            if(IsUpdated)
            {
               for(var i = 0;i<staff.StaffAccess.Count;i++)
                {
                    staff.StaffAccess[i].StaffId = staff.Staff.Id;
                    _Roles.UpdateAccess(staff.StaffAccess[i],count);
                    count++;
                    continue;
                }
                return Ok("Roles updated successfully.");
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteStaff")]
        public IActionResult DeleteStaff([FromBody] Staff staff)
        {
            bool IsDeleted= _Roles.DeleteStaff(staff);
            if(IsDeleted)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddAccess")]
        public IActionResult AddAccess([FromBody] Access access)
        {
          bool IsAdded=_Roles.AddAccess(access);
            if(IsAdded)
            {
                return Ok();
            }
            return BadRequest();
        }

       
        [HttpGet]
        [Route("GetAccessById")]
        public IActionResult GetAccessByid(int Staffid)
        {
            var data = _Roles.GetAccess(Staffid);
            var Staffdata = _Roles.GetStaffByStaffId(Staffid);
          
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new {StaffName=Staffdata.Staff_Name,StaffEamil=Staffdata.Email,StaffRoleTypr=Staffdata.RoleType,AccessValue=data});
            }
        }
    }
}
