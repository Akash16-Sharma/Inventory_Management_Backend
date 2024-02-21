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
    public class UnitTypeController : ControllerBase
    {
        private readonly IUnitType _UnitTypeRepo;
        private readonly IRoles _roles;

        public UnitTypeController(IUnitType unitTypeRepo, IRoles roles)
        {
            _UnitTypeRepo = unitTypeRepo;
            _roles = roles;
        }

        [HttpGet]
        [Route("GetAllUnitTypes")]
        public IActionResult Get(int OrgId, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _UnitTypeRepo.GetAllUnitType(OrgId);
                if (data == null)
                {
                    return NotFound(new { Message = "No unit types found for the given organization." });
                }
                else
                {
                    return Ok(data);
                }
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                for (int i = 0; i < AccessData.Count; i++)
                {
                    if (AccessData[i].SideBarName == "UnitType" && AccessData[i].IsShow == true)
                    {
                        var data = _UnitTypeRepo.GetAllUnitType(OrgId);
                        if (data == null)
                        {
                            return NotFound(new { Message = "No unit types found for the given organization and staff." });
                        }
                        else
                        {
                            return Ok(data);
                        }
                    }
                }
            }
            return BadRequest(new { Message = "Invalid request parameters." });
        }

        //[HttpPost]
        //[Route("AddUnitType")]
        //public IActionResult AddUnitType([FromBody] UnitType unit, int StaffId)
        //{
        //   // unit.UpdatedBy = StaffId;
        //    var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
        //    if (CheckRoleTypeData.RoleType == "Admin")
        //    {
        //        bool IsSaved = _UnitTypeRepo.AddUnitType(unit);
        //        if (IsSaved)
        //        {
        //            return Ok(new { Message = "Unit type added successfully." });
        //        }
        //        else
        //        {
        //            return BadRequest(new { Message = "Failed to add the unit type." });
        //        }
        //    }
        //    else
        //    {
        //        var AccessData = _roles.CheckAccess(StaffId);
        //        for (int i = 0; i < AccessData.Count; i++)
        //        {
        //            if (AccessData[i].SideBarName == "UnitType" && AccessData[i].IsCreate == true)
        //            {
        //                bool IsSaved = _UnitTypeRepo.AddUnitType(unit);
        //                if (IsSaved)
        //                {
        //                    return Ok(new { Message = "Unit type added successfully." });
        //                }
        //                else
        //                {
        //                    return BadRequest(new { Message = "Failed to add the unit type." });
        //                }break;
        //            }
        //        }
        //    }
        //    return BadRequest(new { Message = "Invalid request parameters." });
        //}

        //[HttpPut]
        //[Route("UpdateUnitType")]
        //public IActionResult UpdateUnitType([FromBody] UnitType unit, int StaffId)
        //{
        //    //unit.UpdatedBy = StaffId;
        //    var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
        //    if (CheckRoleTypeData.RoleType == "Admin")
        //    {
        //        bool IsUpdate = _UnitTypeRepo.UpdateUnitType(unit);
        //        if (IsUpdate)
        //        {
        //            return Ok(new { Message = "Unit type updated successfully." });
        //        }
        //        else
        //        {
        //            return BadRequest(new { Message = "Failed to update the unit type." });
        //        }
        //    }
        //    else
        //    {
        //        var AccessData = _roles.CheckAccess(StaffId);
        //        for (int i = 0; i < AccessData.Count; i++)
        //        {
        //            if (AccessData[i].SideBarName == "UnitType" && AccessData[i].IsModify == true)
        //            {
        //                bool IsUpdate = _UnitTypeRepo.UpdateUnitType(unit);
        //                if (IsUpdate)
        //                {
        //                    return Ok(new { Message = "Unit type updated successfully." });
        //                }
        //                else
        //                {
        //                    return BadRequest(new { Message = "Failed to update the unit type." });
        //                }break;
        //            }
        //        }
        //    }
        //    return BadRequest(new { Message = "Invalid request parameters." });
        //}

        //[HttpDelete]
        //[Route("DeleteUnitType")]
        //public IActionResult DeleteUnitType(int id, int StaffId)
        //{
        //    var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
        //    if (CheckRoleTypeData.RoleType == "Admin")
        //    {
        //        bool IsDelete = _UnitTypeRepo.DeleteUnitType(id, StaffId);
        //        if (IsDelete)
        //        {
        //            return Ok(new { Message = "Unit type deleted successfully." });
        //        }
        //        else
        //        {
        //            return BadRequest(new { Message = "Failed to delete the unit type." });
        //        }
        //    }
        //    else
        //    {
        //        var AccessData = _roles.CheckAccess(StaffId);
        //        for (int i = 0; i < AccessData.Count; i++)
        //        {
        //            if (AccessData[i].SideBarName == "UnitType" && AccessData[i].IsModify == true)
        //            {
        //                bool IsDelete = _UnitTypeRepo.DeleteUnitType(id, StaffId);
        //                if (IsDelete)
        //                {
        //                    return Ok(new { Message = "Unit type deleted successfully." });
        //                }
        //                else
        //                {
        //                    return BadRequest(new { Message = "Failed to delete the unit type." });
        //                }break;
        //            }
        //        }
        //    }
        //    return BadRequest(new { Message = "Invalid request parameters." });
        //}

        //[HttpPost]
        //[Route("GetUnitTypeById")]
        //public IActionResult GetUnitTypeById(int id,int StaffId)
        //{
        //    var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
        //    if (CheckRoleTypeData.RoleType == "Admin")
        //    {
        //        var data = _UnitTypeRepo.GetUnitTypeById(id);
        //        if (data == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return Ok(data);
        //        }
        //    }
        //    else
        //    {
        //        var AccessData = _roles.CheckAccess(StaffId);
        //        for (int i = 0; i < AccessData.Count; i++)
        //        {
        //            if (AccessData[i].SideBarName == "UnitType" && AccessData[i].IsModify == true)
        //            {
        //                var data = _UnitTypeRepo.GetUnitTypeById(id);
        //                if (data == null)
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    return Ok(data);
        //                }
        //                break;
        //            }
        //        }
        //    } return BadRequest();
        //}
    }
}
