using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using BackendAPI.Models.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IOrganisation_Info _organisation_info;
        private readonly IRoles _Roles;

        public SettingsController(IOrganisation_Info organisation_info, IRoles roles)
        {
            _organisation_info = organisation_info;
            _Roles = roles;
        }

        [HttpGet]
        [Route("GetOrganisationInfo/{orgid}")]
        public IActionResult GetOrganisationInfo(int orgid, int StaffId)
        {
            var CheckRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _organisation_info.GetOrganisation_Infos(orgid);
                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(data);
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("GetUserInfo")]
        public IActionResult GetUserInfo(int orgid, int StaffId)
        {
            var CheckRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _organisation_info.UserInfo(orgid);
                if (data == null)
                {
                    return Ok(); 
                }
                else
                {
                    return Ok(data);
                }
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("AddUserInfo")]
        public IActionResult AddUserInfo([FromBody] User_Info info, int StaffId)
        {
            var CheckRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool ISAdd = _organisation_info.AddUserInfo(info);
                if (ISAdd)
                {
                    return Ok();
                }
                else
                    return BadRequest();
            }
            else { return BadRequest(); }
        }

        [HttpPut]
        [Route("UpdateOrg")]
        public IActionResult UpdateOrg(Organisation_Info info, int StaffId)
        {
            var CheckRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsUpdate = _organisation_info.Update_Organisation(info);
                if (IsUpdate)
                {
                    return Ok();
                }
                else
                    return BadRequest();
            }
            else
            { return BadRequest(); }
        }

        [HttpPut]
        [Route("UpdateUserInfo")]
        public IActionResult UpdateUserInfo([FromBody] User_Info info, int StaffId)
        {
            var CheckRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool ISUpdate=_organisation_info.UpdateUserInfo(info);
                if (!ISUpdate)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
