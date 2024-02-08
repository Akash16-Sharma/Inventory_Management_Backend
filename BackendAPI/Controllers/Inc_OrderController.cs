using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using BackendAPI.Models.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Inc_OrderController : ControllerBase
    {
        private readonly IInc_Orders _incord;
        private readonly IRoles _roles;

        public Inc_OrderController(IInc_Orders incord, IRoles Roles)
        {
            _incord = incord;
            _roles = Roles;
        }

        [HttpGet]
        [Route("GetOrderInfo")]
        public IActionResult GetOrderInfo(int orgid, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _incord.GetOrderInfo(orgid);
                if (data == null)
                {
                    return NotFound("No order information found.");
                }
                else
                {
                    return Ok(data);
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsShow == true)
                    {
                        var data = _incord.GetOrderInfo(orgid);
                        if (data == null)
                        {
                            return NotFound("No order information found.");
                        }
                        else
                        {
                            return Ok(data);
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPost]
        [Route("AddOrder")]
        public IActionResult AddOrder([FromBody] Inc_Order order, int StaffId)
        {
            order.Updated_By = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsAdd = _incord.AddOrder(order);
                if (IsAdd)
                {
                    return Ok("Order added successfully.");
                }
                else
                {
                    return BadRequest("Failed to add order.");
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsCreate == true)
                    {
                        bool IsAdd = _incord.AddOrder(order);
                        if (IsAdd)
                        {
                            return Ok("Order added successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to add order.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpPut]
        [Route("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] Inc_Order order, int StaffId)
        {
            order.Updated_By = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsUpdate = _incord.UpdateOrder(order);
                if (IsUpdate)
                {
                    return Ok("Order updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update order.");
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsModify == true)
                    {
                        bool IsUpdate = _incord.UpdateOrder(order);
                        if (IsUpdate)
                        {
                            return Ok("Order updated successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to update order.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public IActionResult DeleteOrder(int id, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsDelete = _incord.DeleteOrder(id, StaffId);
                if (IsDelete)
                {
                    return Ok("Order deleted successfully.");
                }
                else
                {
                    return BadRequest("Failed to delete order.");
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsModify == true)
                    {
                        bool IsDelete = _incord.DeleteOrder(id, StaffId);
                        if (IsDelete)
                        {
                            return Ok("Order deleted successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to delete order.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }

        [HttpGet]
        [Route("GetOrderById")]
        public IActionResult GetOrder(int id, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _incord.GetOrderInfoById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return BadRequest("No order information found for the given ID.");
                }
            }
            else
            {
                var Accessdata = _roles.CheckAccess(StaffId);
                for (var i = 0; i < Accessdata.Count; i++)
                {
                    if (Accessdata[i].SideBarName == "Order" && Accessdata[i].IsShow == true)
                    {
                        var data = _incord.GetOrderInfoById(id);
                        if (data != null)
                        {
                            return Ok(data);
                        }
                        else
                        {
                            return BadRequest("No order information found for the given ID.");
                        }
                    }
                }
            }
            return BadRequest("Access denied.");
        }
    }
}
