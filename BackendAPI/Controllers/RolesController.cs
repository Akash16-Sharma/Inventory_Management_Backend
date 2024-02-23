using BackendAPI.IRepository.Roles;
using BackendAPI.Models.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoles _Roles;
        public RolesController(IRoles roles)
        {
            _Roles = roles;
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
        public IActionResult AddStaff([FromBody] StaffRoleRequest Staff)
        {
            
            int StaffId = _Roles.AddStaff(Staff.Staff);
            if(StaffId>0)
            {
                for (var i = 0; i < Staff.StaffAccess.Count; i++)
                {
                    Staff.StaffAccess[i].StaffId = StaffId;
                    bool IsAdded = _Roles.AddAccess(Staff.StaffAccess[i]);
                        continue;
                    
                }
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
            bool IsUpdated=_Roles.UpdateStaff(staff.Staff);
            if(IsUpdated)
            {
               for(var i = 0;i<staff.StaffAccess.Count;i++)
                {
                    staff.StaffAccess[i].StaffId = staff.Staff.Id;
                    bool IsAccessUpdate = _Roles.UpdateAccess(staff.StaffAccess[i]);
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
            if(data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
    }
}
