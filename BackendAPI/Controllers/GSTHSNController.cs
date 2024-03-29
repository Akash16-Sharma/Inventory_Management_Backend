using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GSTHSNController : ControllerBase
    {
        private readonly IGST_HSN_SAC _gsthsn;
        private readonly IRoles _roles;

        public GSTHSNController(IGST_HSN_SAC gsthsn, IRoles roles)
        {
            _gsthsn = gsthsn;
            _roles = roles;
        }

        [HttpGet]
        [Route("GetGSTInfo")]
        public IActionResult GetGSTInfo(int orgid)
        {
            var data = _gsthsn.GetGstInfo(orgid);
            if (data == null)
                return NotFound("No GST information found for the specified organization.");
            return Ok(data);
        }

        [HttpPost]
        [Route("AddGstInfo")]
        public IActionResult AddGstInfo(Gst gst, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsSaved = _gsthsn.AddGST(gst);
                if (IsSaved)
                    return Ok("GST information added successfully.");
                return BadRequest("Failed to add GST information.");
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                foreach (var access in AccessData)
                {
                    if (access.SideBarName == "GST" && access.IsCreate)
                    {
                        bool IsSaved = _gsthsn.AddGST(gst);
                        if (IsSaved)
                            return Ok("GST information added successfully.");
                        else
                            return BadRequest("Failed to add GST information.");
                    }
                }
            }
            return BadRequest("Insufficient permissions to add GST information.");
        }

        [HttpPut]
        [Route("UpdateGST")]
        public IActionResult UpdateGST(Gst gst, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsSaved = _gsthsn.Update_GST_INFO(gst);
                if (IsSaved)
                    return Ok("GST information updated successfully.");
                return BadRequest("Failed to update GST information.");
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                foreach (var access in AccessData)
                {
                    if (access.SideBarName == "GST" && access.IsModify)
                    {
                        bool IsSaved = _gsthsn.Update_GST_INFO(gst);
                        if (IsSaved)
                            return Ok("GST information updated successfully.");
                        else
                            return BadRequest("Failed to update GST information.");
                    }
                }
            }
            return BadRequest("Insufficient permissions to update GST information.");
        }

        [HttpDelete]
        [Route("DeleteGSTInfo")]
        public IActionResult DeleteGST(int id, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsDeleted = _gsthsn.Delete_GST_INFO(id);
                if (IsDeleted)
                    return Ok("GST information deleted successfully.");
                return BadRequest("Failed to delete GST information.");
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                foreach (var access in AccessData)
                {
                    if (access.SideBarName == "GST" && access.IsModify)
                    {
                        bool IsDeleted = _gsthsn.Delete_GST_INFO(id);
                        if (IsDeleted)
                            return Ok("GST information deleted successfully.");
                        else
                            return BadRequest("Failed to delete GST information.");
                    }
                }
            }
            return BadRequest("Insufficient permissions to delete GST information.");
        }

        [HttpGet]
        [Route("GetGstInfoById")]
        public IActionResult GetGST(int id)
        {
            var data = _gsthsn.GetGST(id);
            if (data == null)
                return BadRequest("No GST information found with the specified ID.");
            return Ok(data);
        }

        [HttpGet]
        [Route("GetHSN")]
        public IActionResult GetHSN(int orgid)
        {
            var data = _gsthsn.GetHSNInfo(orgid);
            if (data == null)
                return BadRequest("No HSN information found for the specified organization.");
            return Ok(data);
        }

        [HttpPost]
        [Route("AddHSNInfo")]
        public IActionResult AddHSNInfo(Hsn hsn, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsSaved = _gsthsn.AddHSN(hsn);
                if (IsSaved)
                    return Ok("HSN information added successfully.");
                return BadRequest("Failed to add HSN information.");
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                foreach (var access in AccessData)
                {
                    if (access.SideBarName == "HSN" && access.IsCreate)
                    {
                        bool IsSaved = _gsthsn.AddHSN(hsn);
                        if (IsSaved)
                            return Ok("HSN information added successfully.");
                        else
                            return BadRequest("Failed to add HSN information.");
                    }
                }
            }
            return BadRequest("Insufficient permissions to add HSN information.");
        }


        [HttpPut]
        [Route("UpdateHSN")]
        public IActionResult UpdateHSN(Hsn hsn, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsUpdate = _gsthsn.UpdateHSN(hsn);
                if (IsUpdate)
                    return Ok("HSN information updated successfully.");
                return BadRequest("Failed to update HSN information.");
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                foreach (var access in AccessData)
                {
                    if (access.SideBarName == "HSN" && access.IsModify)
                    {
                        bool IsUpdate = _gsthsn.UpdateHSN(hsn);
                        if (IsUpdate)
                            return Ok("HSN information updated successfully.");
                        else
                            return BadRequest("Failed to update HSN information.");
                    }
                }
            }
            return BadRequest("Insufficient permissions to update HSN information.");
        }

        [HttpDelete]
        [Route("DeleteHSN")]
        public IActionResult DeleteHSN(int id, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsDelete = _gsthsn.DeleteHSN(id);
                if (IsDelete)
                    return Ok("HSN information deleted successfully.");
                return BadRequest("Failed to delete HSN information.");
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                foreach (var access in AccessData)
                {
                    if (access.SideBarName == "HSN" && access.IsModify)
                    {
                        bool IsDelete = _gsthsn.DeleteHSN(id);
                        if (IsDelete)
                            return Ok("HSN information deleted successfully.");
                        else
                            return BadRequest("Failed to delete HSN information.");
                    }
                }
            }
            return BadRequest("Insufficient permissions to delete HSN information.");
        }


        [HttpGet]
        [Route("GetHSNById")]
        public IActionResult GetHSNById(int id)
        {
            var data = _gsthsn.GetHSN(id);
            if (data == null)
                return NotFound("No HSN information found with the specified ID.");
            return Ok(data);
        }
    }
}
