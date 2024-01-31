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
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _Category;
        private readonly IRoles _roles;

        public CategoryController(ICategory category, IRoles roles)
        {
            _Category = category;
            _roles = roles;
        }


        [HttpGet]
        [Route("Get")]
        public IActionResult Get(int OrgId, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var Catdata = _Category.GetAllCategory(OrgId);
                if (Catdata == null)
                {
                    return NotFound(new { Message = "No categories found for the given organization." });
                }
                else
                {
                    return Ok(Catdata);
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {

                    if (Accessdata[i].SideBarName == "Category" && Accessdata[i].IsShow == true)
                    {
                        var data = _Category.GetAllCategory(OrgId);
                        if (data == null)
                        {
                            return NotFound(new { Message = "No categories found for the given organization and staff." });
                           
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
    

           
        

        [HttpPost]
        [Route("AddCategory")]
        public IActionResult AddCategory([FromBody] Category category,int StaffId)
        {
            category.UpdatedBy = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsSave = _Category.AddCategory(category);
                if (IsSave)
                {
                    return Ok(new { Message = "Category added successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to add the category." });
                }
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                for (int i = 0; i < AccessData.Count; i++)
                {
                    if (AccessData[i].SideBarName == "Category" && AccessData[i].IsCreate == true)
                    {
                        bool IsSave = _Category.AddCategory(category);
                        if (IsSave)
                        {
                            return Ok(new { Message = "Category added successfully." });
                        }
                        else
                        {
                            return BadRequest(new { Message = "Failed to add the category." });
                        }
                       
                    }
                }

            }
            return BadRequest(new { Message = "Invalid request parameters." });
        } 

        [HttpPut]
        [Route("EditCategory")]
        public IActionResult EditCategory([FromBody] Category category,int StaffId)
        {
            category.UpdatedBy = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
               
                bool IsUpdate = _Category.UpdateCategory(category);
                if (IsUpdate)
                {
                    return Ok(new { Message = "Category updated successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to update the category." });
                }
            }
            else
            {
                
                var AccessData = _roles.CheckAccess(StaffId);
                for (int i = 0; i < AccessData.Count; i++)
                {
                    if (AccessData[i].SideBarName == "Category" && AccessData[i].IsModify == true)
                    {
                       
                        bool IsUpdate = _Category.UpdateCategory(category);
                        if (IsUpdate)
                        {
                            return Ok(new { Message = "Category updated successfully." });
                        }
                        else
                        {
                            return BadRequest(new { Message = "Failed to update the category." });
                        }
                    }
                }
            }
            return BadRequest(new { Message = "Invalid request parameters." });
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        public IActionResult DeleteCategory(int id,int staffid)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(staffid);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsDelete = _Category.DeleteCategory(id,staffid);
                if (IsDelete)
                {
                    return Ok(new { Message = "Category deleted successfully." });
                }
                return BadRequest(new { Message = "Failed to delete the category." });
            }
            else
            {
                var AccessData = _roles.CheckAccess(staffid);
                for (int i = 0; i < AccessData.Count; i++)
                {
                    if (AccessData[i].SideBarName == "Category" && AccessData[i].IsModify == true)
                    {
                        bool IsDelete = _Category.DeleteCategory(id,staffid);
                        if (IsDelete)
                        {
                            return Ok(new { Message = "Category deleted successfully." });
                        }
                        return BadRequest(new { Message = "Failed to delete the category." });
                    }
                }
            }
            return BadRequest(new { Message = "Invalid request parameters." });
        }
    }
}
