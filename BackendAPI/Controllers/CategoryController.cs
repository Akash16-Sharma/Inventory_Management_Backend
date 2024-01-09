using BackendAPI.IRepository;
using BackendAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _Category;
        public CategoryController(ICategory category)
        {
            _Category = category;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var data=_Category.GetAllCategory();
            if(data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpPost]
        [Route("AddCategory")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            bool IsSave=_Category.AddCategory(category);
            if (IsSave)
            {
                return Ok(new { Message = "Category Added Successfully" });
            }
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("EditCategory")]
        public IActionResult EditCategory([FromBody] Category category)
        {
            bool IsUpdate=_Category.UpdateCategory(category);
            if (IsUpdate)
            {
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            bool IsDelete=_Category.DeleteCategory(id);
            if (IsDelete)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
