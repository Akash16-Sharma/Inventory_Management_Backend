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

        [HttpPost]
        [Route("AddItem")]
        public void AddItem([FromForm] ItemWithImage itemWithImage)
        {
        }




        [HttpPost]
        [Route("UpdateItem/{itemId}")]
        public IActionResult UpdateItem(int itemId, [FromForm] ItemWithImage itemWithImage)
        {
            try
            {
                // Retrieve the existing item from the repository
                Item existingItem = _itemRepo.GetItemById(itemId);

                if (existingItem == null)
                {
                    return NotFound(new { Status = 404, Message = "Item not found" });
                }

                // Check if the request contains a new image file
                if (itemWithImage.ImageFile != null && itemWithImage.ImageFile.Length > 0)
                {
                    // Generate a unique filename for the new image
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(itemWithImage.ImageFile.FileName);

                    // Specify the folder where the new image will be saved
                    string uploadPath = Path.Combine("wwwroot/images", fileName);

                    // Save the new image to the specified folder
                    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                    {
                        itemWithImage.ImageFile.CopyTo(fileStream);
                    }

                    // Update the existing item's image path
                   // existingItem.ImagePath = fileName;
                }

                // Update other properties of the existing item
                existingItem = itemWithImage.Item;

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
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = 500, Message = $"Internal Server Error: {ex.Message}" });
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
