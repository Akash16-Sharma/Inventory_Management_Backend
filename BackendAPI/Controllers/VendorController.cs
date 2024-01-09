using BackendAPI.IRepository;
using BackendAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendor _Vendor;
        public VendorController(IVendor vendor)
        {
            _Vendor = vendor;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var vendor = _Vendor.GetVendor();
            if (vendor == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(vendor);
            }
        }

        [HttpPost]
        [Route("AddVendor")]
        public IActionResult AddVendor([FromBody]Vendor vendor)
        {
            bool IsSave=_Vendor.AddVendor(vendor);
            if(IsSave)
            {
                return Ok();
            }
            else
            { return BadRequest(); }    
        }

        [HttpPost]
        [Route("UpdateVendor")]
        public IActionResult UpdateVendor([FromBody] Vendor vendor)
        {
            bool IsUpdate=_Vendor.UpdateVendor(vendor);
            if(IsUpdate)
            {
                return Ok();
            }
            else { return BadRequest(); }
        }

        [HttpPost]
        [Route("DeleteVendor")]
        public IActionResult DeleteVendor(int id)
        {
            bool IsDelete=_Vendor.RemoveVendor(id);
            if(IsDelete)
            {
                return Ok();
            }
            else return BadRequest();
        }

    }
}
