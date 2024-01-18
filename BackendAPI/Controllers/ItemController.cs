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
                    if (AccessData[i].SideBarName == "Item" && AccessData[i].Read_Access == true)
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
                    if (AccessData[i].SideBarName == "Item" && AccessData[i].Create_Access == true)
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

        [HttpPost]
        [Route("UpdateItem/{itemId}")]
        public IActionResult UpdateItem(Item item, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var existingItem = _itemRepo.GetItemById(item.Id);
                if (existingItem == null)
                {
                    return BadRequest(new { Message = "Item not found." });
                }

                existingItem.Name = item.Name;
                existingItem.Updated_By = item.Updated_By;
                existingItem.Stock_Alert = item.Stock_Alert;
                existingItem.Selling_Price = item.Selling_Price;
                existingItem.Buying_Price = item.Buying_Price;
                existingItem.Opening_Stock = item.Opening_Stock;
                existingItem.Barcode = item.Barcode;
                existingItem.Vendor_Id = item.Vendor_Id;
                existingItem.Unit_Type_Id = item.Unit_Type_Id;
                existingItem.Category_Id = item.Category_Id;
                existingItem.InsertedOn = DateTime.Now;

                // Save the updated item to the repository
                bool IsUpdated = _itemRepo.UpdateItem(existingItem);

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
                    if (AccessData[i].SideBarName == "Item" && AccessData[i].Update_Access == true)
                    {
                        var existingItem = _itemRepo.GetItemById(item.Id);
                        if (existingItem == null)
                        {
                            return BadRequest(new { Message = "Item not found." });
                        }

                        existingItem.Name = item.Name;
                        existingItem.Updated_By = item.Updated_By;
                        existingItem.Stock_Alert = item.Stock_Alert;
                        existingItem.Selling_Price = item.Selling_Price;
                        existingItem.Buying_Price = item.Buying_Price;
                        existingItem.Opening_Stock = item.Opening_Stock;
                        existingItem.Barcode = item.Barcode;
                        existingItem.Vendor_Id = item.Vendor_Id;
                        existingItem.Unit_Type_Id = item.Unit_Type_Id;
                        existingItem.Category_Id = item.Category_Id;
                        existingItem.InsertedOn = DateTime.Now;

                        // Save the updated item to the repository
                        bool IsUpdated = _itemRepo.UpdateItem(existingItem);

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

        [HttpPost]
        [Route("DeleteItem")]
        public IActionResult DeleteItem([FromBody] Item item, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsDelete = _itemRepo.DeleteItem(item);
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
                    if (AccessData[i].SideBarName == "Item" && AccessData[i].Delete_Access == true)
                    {
                        bool IsDelete = _itemRepo.DeleteItem(item);
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
    }
}
