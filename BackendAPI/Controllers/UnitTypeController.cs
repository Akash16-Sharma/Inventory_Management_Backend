using BackendAPI.IRepository;
using BackendAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitTypeController : ControllerBase
    {
        private readonly IUnitType _UnitTypeRepo;

        public UnitTypeController(IUnitType unitTypeRepo)
        {
            _UnitTypeRepo = unitTypeRepo;
        }

        [HttpGet]
        [Route("GetAllUnitTypes")]
        public IActionResult Get() 
        {
            var data = _UnitTypeRepo.GetAllUnitType();
            if(data == null)
            {
                return NotFound();
            }
            else
                return Ok(data);
        }

        [HttpPost]
        [Route("AddUnitType")]
        public IActionResult AddUnitType([FromBody]UnitType unit)
        {
            bool IsSaved=_UnitTypeRepo.AddUnitType(unit);
            if(IsSaved)
            {
                return Ok();
            }
            else { return BadRequest(); }
        }

        [HttpPost]
        [Route("UpdateUnitType")]
        public IActionResult UpdateUnitType([FromBody]UnitType unit)
        {
            bool IsUpdate=_UnitTypeRepo.UpdateUnitType(unit);
            if(IsUpdate)
            {
                return Ok();
            }
            else return BadRequest();
        }

        [HttpPost]
        [Route("DeleteUnitType")]
        public IActionResult DeleteUnitType(int id)
        {
            bool IsDelete=_UnitTypeRepo.DeleteUnitType(id);
            if(IsDelete)
            {
                return Ok();
            }
            else
                return BadRequest();
        }
    }
}
