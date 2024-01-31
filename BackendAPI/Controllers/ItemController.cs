using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using BackendAPI.Models.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepo _itemRepo;
        private readonly IRoles _roles;

        public ItemController(IItemRepo itemRepo, IRoles roles)
        {
            _itemRepo = itemRepo;
            _roles = roles;
        }

        [HttpGet]
        [Route("GetItemInfo")]
        public IActionResult GetItemInfo(int orgid, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _itemRepo.GetItemInfo(orgid);
                if (data == null)
                {
                    return NotFound(new { Message = "No items found for the given organization." });
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
                    if (AccessData[i].SideBarName == "Item" && AccessData[i].IsShow == true)
                    {
                        var data = _itemRepo.GetItemInfo(orgid);
                        if (data == null)
                        {
                            return NotFound(new { Message = "No items found for the given organization and staff." });
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
        [Route("AddItem")]
        public IActionResult AddItem([FromBody] Item item, int StaffId)
        {
            if(item.Unit_Type_Id==0||item.Category_Id==0||item.Vendor_Id==0)
            {
                return BadRequest();
            }
            item.Updated_By = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsSaved = _itemRepo.AddItem(item);
                if (IsSaved)
                {
                    return Ok(new { Message = "Item added successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to add the item." });
                }
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                for (int i = 0; i < AccessData.Count; i++)
                {
                    if (AccessData[i].SideBarName == "Item" && AccessData[i].IsCreate == true)
                    {
                        bool IsSaved = _itemRepo.AddItem(item);
                        if (IsSaved)
                        {
                            return Ok(new { Message = "Item added successfully." });
                        }
                        else
                        {
                            return BadRequest(new { Message = "Failed to add the item." });
                        }
                    }
                }
            }
            return BadRequest(new { Message = "Invalid request parameters." });
        }

        [HttpPut]
        [Route("UpdateItem")]
        public IActionResult UpdateItem([FromBody]Item item, int StaffId)
        {
            item.Updated_By = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
               
                // Save the updated item to the repository
                bool IsUpdated = _itemRepo.UpdateItem(item);

                if (IsUpdated)
                {
                    return Ok(new { Status = 200, Message = "Item updated successfully." });
                }
                else
                {
                    return BadRequest(new { Status = 400, Message = "Failed to update the item." });
                }
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                for (int i = 0; i < AccessData.Count; i++)
                {
                    if (AccessData[i].SideBarName == "Item" && AccessData[i].IsModify == true)
                    {
                        
                        // Save the updated item to the repository
                        bool IsUpdated = _itemRepo.UpdateItem(item);

                        if (IsUpdated)
                        {
                            return Ok(new { Status = 200, Message = "Item updated successfully." });
                        }
                        else
                        {
                            return BadRequest(new { Status = 400, Message = "Failed to update the item." });
                        }
                    }
                }
            }
            return BadRequest(new { Message = "Invalid request parameters." });
        }

        [HttpDelete]
        [Route("DeleteItem")]
        public IActionResult DeleteItem(int id, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsDelete = _itemRepo.DeleteItem(id, StaffId);
                if (IsDelete)
                {
                    return Ok(new { Status = 200, Message = "Item deleted successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to delete the item." });
                }
            }
            else
            {
                var AccessData = _roles.CheckAccess(StaffId);
                for (int i = 0; i < AccessData.Count; i++)
                {
                    if (AccessData[i].SideBarName == "Item" && AccessData[i].IsModify == true)
                    {
                        bool IsDelete = _itemRepo.DeleteItem(id, StaffId);
                        if (IsDelete)
                        {
                            return Ok(new { Status = 200, Message = "Item deleted successfully." });
                        }
                        else
                        {
                            return BadRequest(new { Message = "Failed to delete the item." });
                        }
                    }
                }
            }
            return BadRequest(new { Message = "Invalid request parameters." });
        }


        [HttpGet]
        [Route("GetItemById")]
        public IActionResult GetItem(int id, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var ItemData = _itemRepo.GetItemById(id);
                if (ItemData == null)
                {
                    return NotFound();
                }
                else { return Ok(ItemData);}
            }
            else
            {

                var AccessData = _roles.CheckAccess(StaffId);
                for (int i = 0; i < AccessData.Count; i++)
                {
                    if (AccessData[i].SideBarName == "Item" && AccessData[i].IsModify == true)
                    {
                        var ItemData = _itemRepo.GetItemById(id);
                        if (ItemData == null)
                        {
                            return NotFound();
                        }
                        else { return Ok(ItemData); }
                    }
                }
            }
            return BadRequest();
        }
        }
}
