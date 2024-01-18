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
        public IActionResult AddStaff([FromBody]Staff staff)
        {
           bool IsSaved= _Roles.AddStaff(staff);
            if(IsSaved)
            {
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("UpdateStaff")]
        public IActionResult UpdateStaff([FromBody]Staff staff)
        {
            bool IsUpdated=_Roles.UpdateStaff(staff);
            if(IsUpdated)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
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
    }
}
