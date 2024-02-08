using BackendAPI.IRepository;
using BackendAPI.IRepository.Roles;
using BackendAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Out_OrderController : ControllerBase
    {
        private readonly IOut_Order _OutOrder;
        private readonly IRoles _roles;
        public Out_OrderController(IOut_Order outOrder,IRoles roles)
        {
            _OutOrder = outOrder;
            _roles = roles;
        }

        [HttpGet]
        [Route("GetOrderInfo")]
        public IActionResult GetOrderInfo(int orgid, int StaffId)
        {
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                var data = _OutOrder.GetOutOrders(orgid);
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
                        var data = _OutOrder.GetOutOrders(orgid);
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
        public IActionResult AddOrder([FromBody] Out_Order order, int StaffId)
        {
            order.Updated_By = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsAdd = _OutOrder.AddOrder(order);
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
                        bool IsAdd = _OutOrder.AddOrder(order);
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
        public IActionResult UpdateOrder([FromBody] Out_Order order, int StaffId)
        {
            order.Updated_By = StaffId;
            var CheckRoleTypeData = _roles.CheckStaffType(StaffId);
            if (CheckRoleTypeData.RoleType == "Admin")
            {
                bool IsUpdate = _OutOrder.UpdateOrder(order);
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
                        bool IsUpdate = _OutOrder.UpdateOrder(order);
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
                bool IsDelete = _OutOrder.DeleteOrder(id, StaffId);
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
                        bool IsDelete = _OutOrder.DeleteOrder(id, StaffId);
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
                var data = _OutOrder.GetOrderById(id);
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
                        var data = _OutOrder.GetOrderById(id);
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
    

