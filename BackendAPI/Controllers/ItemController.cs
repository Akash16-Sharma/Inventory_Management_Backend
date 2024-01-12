using BackendAPI.IRepository;
using BackendAPI.Models;
using BackendAPI.Models.Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepo _itemRepo;

        public ItemController(IItemRepo itemRepo)
        {
            _itemRepo = itemRepo;
        }



        [HttpGet]
        [Route("GetItemInfo")]
        public IActionResult GetItemInfo(int orgid)
        {
            var data= _itemRepo.GetItemInfo(orgid);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
        

        [HttpPost]
        [Route("AddItem")]
        public IActionResult AddItem([FromBody] Item item)
        {
            bool IsSaved=_itemRepo.AddItem(item);
            if (IsSaved)
            {
                return Ok();
            }
            else { return BadRequest(); }
        }


        [HttpPost]
        [Route("UpdateItem/{itemId}")]
        public IActionResult UpdateItem(Item item)
        {

            var existingItem=_itemRepo.GetItemById(item.Id);
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
                    return Ok(new { Status = 200, Message = "Updated Successfully" });
                }
                else
                {
                    return BadRequest(new { Status = 400, Message = "Failed to update item" });
                }
            }
           
        


        [HttpPost]
        [Route("DeleteItem")]
        public IActionResult DeleteItem(int id)
        {
            _itemRepo.DeleteItem(id);
            bool IsDelete=_itemRepo.DeleteItem(id);
            if (IsDelete)
            {
                return Ok(new { Status = 200, Message = "Inserted Successfully" });
            }
            else
            { return BadRequest(); }
        }
    }
}
