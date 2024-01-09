using BackendAPI.IRepository;
using BackendAPI.Models;
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
        public IActionResult AddItem([FromBody]Item items)
        {
            bool IsSaved=_itemRepo.AddItem(items);
            if (IsSaved )
            {
                return Ok(new { Status = 200, Message = "Inserted Successfully" });
            }
            else
            { return BadRequest(); }
        }

        [HttpPost]
        [Route("UpdateItem")]
        public IActionResult UpdateItem([FromBody]Item item) 
        {
            bool IsUpdate=_itemRepo.UpdateItem(item);
            if (IsUpdate)
            {
                return Ok(new { Status = 200, Message = "Inserted Successfully" });
            }
            else
            { return BadRequest(); }
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
