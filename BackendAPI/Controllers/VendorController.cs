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
    public class VendorController : ControllerBase
    {
        private readonly IVendor _Vendor;
        private readonly IRoles _Roles;

        public VendorController(IVendor vendor, IRoles roles)
        {
            _Vendor = vendor;
            _Roles = roles;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get(int OrgId, int StaffId)
        {
            var checkRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (checkRoleTypeData.RoleType == "Admin")
            {
                var vendor = _Vendor.GetVendor(OrgId);
                if (vendor == null)
                {
                    return NotFound("No vendor found for the specified organization.");
                }
                else
                {
                    return Ok(vendor);
                }
            }
            else
            {
                var accessData = _Roles.CheckAccess(StaffId);
                for (var i = 0; i < accessData.Count; i++)
                {
                    if (accessData[i].SideBarName == "Vendor" && accessData[i].Read_Access)
                    {
                        var vendor = _Vendor.GetVendor(OrgId);
                        if (vendor == null)
                        {
                            return NotFound("No vendor found for the specified organization.");
                        }
                        else
                        {
                            return Ok(vendor);
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPost]
        [Route("AddVendor")]
        public IActionResult AddVendor([FromBody] Vendor vendor, int StaffId)
        {
            var checkRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (checkRoleTypeData.RoleType == "Admin")
            {
                bool isSave = _Vendor.AddVendor(vendor);
                if (isSave)
                {
                    return Ok("Vendor added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add vendor.");
                }
            }
            else
            {
                var accessData = _Roles.CheckAccess(StaffId);
                for (var i = 0; i < accessData.Count; i++)
                {
                    if (accessData[i].SideBarName == "Vendor" && accessData[i].Create_Access)
                    {
                        bool isSave = _Vendor.AddVendor(vendor);
                        if (isSave)
                        {
                            return Ok("Vendor added successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to add vendor.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPost]
        [Route("UpdateVendor")]
        public IActionResult UpdateVendor([FromBody] Vendor vendor, int StaffId)
        {
            var checkRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (checkRoleTypeData.RoleType == "Admin")
            {
                bool isUpdate = _Vendor.UpdateVendor(vendor);
                if (isUpdate)
                {
                    return Ok("Vendor updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update vendor.");
                }
            }
            else
            {
                var accessData = _Roles.CheckAccess(StaffId);
                for (var i = 0; i < accessData.Count; i++)
                {
                    if (accessData[i].SideBarName == "Vendor" && accessData[i].Create_Access)
                    {
                        bool isUpdate = _Vendor.UpdateVendor(vendor);
                        if (isUpdate)
                        {
                            return Ok("Vendor updated successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to update vendor.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPost]
        [Route("DeleteVendor")]
        public IActionResult DeleteVendor(Vendor vendor, int StaffId)
        {
            var checkRoleTypeData = _Roles.CheckStaffType(StaffId);
            if (checkRoleTypeData.RoleType == "Admin")
            {
                bool isDelete = _Vendor.RemoveVendor(vendor);
                if (isDelete)
                {
                    return Ok("Vendor deleted successfully.");
                }
                else
                {
                    return BadRequest("Failed to delete vendor.");
                }
            }
            else
            {
                var accessData = _Roles.CheckAccess(StaffId);
                for (var i = 0; i < accessData.Count; i++)
                {
                    if (accessData[i].SideBarName == "Vendor" && accessData[i].Create_Access)
                    {
                        bool isDelete = _Vendor.RemoveVendor(vendor);
                        if (isDelete)
                        {
                            return Ok("Vendor deleted successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to delete vendor.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }
    }
}
